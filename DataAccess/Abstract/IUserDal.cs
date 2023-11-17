using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace DataAccess.Abstract;

public interface IUserDal:IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
    List<UserForTable> GetForList();
}