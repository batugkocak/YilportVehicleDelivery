using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Concrete;

    public class AuthManager:IAuthService
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

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                Username = userForRegisterDto.Username,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
    
            var addedUser = _userService.Add(user);
            _userOperationClaim.Add(
                new UserOperationClaim()
                {
                    OperationClaimId = userForRegisterDto.roleId,
                    UserId = addedUser.Id,
                }
            );
            
            return  new SuccessDataResult<User>(user,Messages.UserRegistered);
        }
        
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUsername(userForLoginDto.Username);
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck,Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByUsername(email)!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }
    }