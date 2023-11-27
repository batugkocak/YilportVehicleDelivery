using Azure;
using Business.Abstract;
using Business.ApiResponses;
using Business.Constants;
using Castle.Core.Resource;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using ServiceStack.Web;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Verify.Ldap.Domain.ServiceModel.Request.Yilport;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private IUserService _userService;
    private IUserOperationClaimService _userOperationClaim;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperationClaim)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _userOperationClaim = userOperationClaim;

    }

    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
    {
        byte[] passwordHash, passwordSalt;

        if (userForRegisterDto.VerificationType == (int)VerificationType.Form)
        {
            if (userForRegisterDto.Password == null)
            {
                return new ErrorDataResult<User>(null, Messages.PasswordRequired);
            }
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
        }
        else
        {
            passwordHash = null;
            passwordSalt = null;
        }

        var user = new User
        {
            Username = userForRegisterDto.Username,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            VerificationType = (byte) userForRegisterDto.VerificationType,
            Status = true,
            Creator = userForRegisterDto.Creator
        };
        var addedUser = _userService.Add(user);
        _userOperationClaim.Add(
            new UserOperationClaim()
            {
                OperationClaimId = userForRegisterDto.RoleId,
                UserId = addedUser.Id,
            }
        );

        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }

    public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByUsername(userForLoginDto.Username);


        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (userToCheck.VerificationType == (byte)VerificationType.Ldap)
        {

            var response = await AuthenticateLdapUserAsync(userForLoginDto);
            if (response.Success)
            {
                return new SuccessDataResult<User>(userToCheck, response.Message);
            }
            return new ErrorDataResult<User>(response.Message);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IDataResult<User> ChangePassword(UserForPasswordChange userForPasswordChange)
    {
        var userToCheck = _userService.GetByUsername(userForPasswordChange.Username);
        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }
        else if(userToCheck.VerificationType == (byte)VerificationType.Ldap)
        {
            return new ErrorDataResult<User>(Messages.LdapUserHasNoPassword);
        }

        if (!HashingHelper.VerifyPasswordHash(userForPasswordChange.OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userForPasswordChange.NewPassword, out passwordHash, out passwordSalt);

        userToCheck.PasswordHash = passwordHash;
        userToCheck.PasswordSalt = passwordSalt;
        _userService.Update(userToCheck);

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulPasswordChange);
    }


    public IResult DeleteUser(int id)
    {
        var userToDelete = _userService.GetById(id);
        if (userToDelete.Data != null)
        {
            _userOperationClaim.DeleteByUserId(id);
        }

        _userService.Delete(id);

        return new SuccessResult(Messages.UserDeleted);
    }

    public IResult UserExists(string username)
    {
        if (_userService.GetByUsername(username) != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }
        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(IDataResult<User> userDataResult)
    {
        if (userDataResult.Data == null)
        {
            return new ErrorDataResult<AccessToken>(null, userDataResult.Message);

        }
        var claims = _userService.GetClaims(userDataResult.Data);
        var accessToken = _tokenHelper.CreateToken(userDataResult.Data, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }

    //TODO: refactor httpclient usage to more optimized way
    public async Task<IResult> AuthenticateLdapUserAsync(UserForLoginDto userForLoginDto)
    {
        var getLdapAuthenticate = new GetLdapAuthenticate
        {
            UserName = userForLoginDto.Username,
            Password = userForLoginDto.Password
        };
        var serializedItem = System.Text.Json.JsonSerializer.Serialize(getLdapAuthenticate);

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(serializedItem, Encoding.UTF8, "application/json");
        LdapResponse result;

        try
        {
            var response = await httpClient.PostAsync("http://ldap.verify.api.test.yph.inc/GetLdapAuthenticate", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<LdapResponse>(responseString);
        }
        catch (Exception e)
        {
            return new ErrorResult("Ldap isteðinde bir hata meydana geldi: " + e.Message);
        }


        if (result == null)
        {
            return new ErrorResult("Bir hata meydana geldi.");

        }
        else if (result.responseCode == "Fail")
        {
            return new ErrorResult(Messages.PasswordError);
        }

        return new SuccessResult("Ldap" + Messages.SuccessfulLogin);
    }

}