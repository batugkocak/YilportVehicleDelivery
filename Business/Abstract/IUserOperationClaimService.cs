using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IUserOperationClaimService
{
    IResult Add(UserOperationClaim userOperationClaim);
}