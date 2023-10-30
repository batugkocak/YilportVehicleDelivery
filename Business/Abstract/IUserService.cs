using Core.Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User GetByUsername(string username);
}