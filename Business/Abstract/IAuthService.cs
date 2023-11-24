using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto);

    Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
    IDataResult<User> ChangePassword(UserForPasswordChange userForPasswordChange);

    IResult DeleteUser(int id);
    IResult UserExists(string username);
    IDataResult<AccessToken> CreateAccessToken(IDataResult<User> dataResult);
}