using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private IUserOperationClaimDal _operationClaimDal;

    public UserOperationClaimManager(IUserOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;
    }
    public IResult Add(UserOperationClaim userOperationClaim)
    {
        _operationClaimDal.Add(userOperationClaim);
        return new SuccessResult(Messages.RoleAdded);
    }
}