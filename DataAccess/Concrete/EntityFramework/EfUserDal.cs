using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.DTOs.User;

namespace DataAccess.Concrete;

public class EfUserDal:EfEntityRepositoryBase<User, VehicleDeliveryContext>,IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new VehicleDeliveryContext())
        {
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim {Id = operationClaim.Id, Name = operationClaim.Name};
            return result.ToList();

        }
    }

    public List<UserForList> GetForList()
    {
        using (var context = new VehicleDeliveryContext())
        {
            var result = from user in context.Users
                join userOperationClaim in context.UserOperationClaims
                    on user.Id equals userOperationClaim.UserId
                join operationClaim in context.OperationClaims
                    on userOperationClaim.OperationClaimId equals operationClaim.Id
                where user.IsDeleted != true
                orderby operationClaim.Name ascending 
                select new UserForList()
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = operationClaim.Name,
                    VerificationType = "Test"
                };
            return result.ToList();
        }
    }
}