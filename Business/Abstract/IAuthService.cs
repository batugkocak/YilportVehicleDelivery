using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto,string password);
    
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string username);
    IDataResult<AccessToken> CreateAccessToken(User user);
}