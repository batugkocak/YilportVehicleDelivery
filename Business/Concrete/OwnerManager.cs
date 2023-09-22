using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class OwnerManager: IOwnerService
{
    private IOwnerDal _ownerDal;

    public OwnerManager(IOwnerDal ownerDal)
    {
        _ownerDal = ownerDal;
    }
    public IDataResult<Owner> GetById(int ownerId)
    {
        return new SuccessDataResult<Owner>(_ownerDal.Get(o => o.Id == ownerId));

    }
}