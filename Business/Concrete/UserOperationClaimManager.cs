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

    public IResult DeleteByUserId(int id)
    {
        var deletedUOCs = _operationClaimDal.GetAll(uoc => uoc.UserId == id);
        foreach (var uoc in deletedUOCs)
        {
            _operationClaimDal.Delete(uoc);
        }
        return new SuccessResult(Messages.RoleDeleted);
    }
}