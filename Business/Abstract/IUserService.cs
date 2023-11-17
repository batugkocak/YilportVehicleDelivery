using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs.User;

namespace Business.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);
    User Add(User user);
    User GetByUsername(string username);

    IDataResult<List<UserForTable>> GetForList();
    
    IDataResult<User> GetById(int userId);
    
    IResult Delete(int id);
    
}