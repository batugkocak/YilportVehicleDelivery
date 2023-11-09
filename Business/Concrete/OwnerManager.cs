using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class OwnerManager: IOwnerService
{
    private IOwnerDal _ownerDal;

    public OwnerManager(IOwnerDal ownerDal)
    {
        _ownerDal = ownerDal;
    }

    public IDataResult<List<Owner>> GetAll()
    {
        return new SuccessDataResult<List<Owner>>(_ownerDal.GetAll(), Messages.OwnersListed);
    }

    public IDataResult<Owner> GetById(int ownerId)
    {
        return new SuccessDataResult<Owner>(_ownerDal.Get(d => d.Id == ownerId), Messages.OwnerListed);
    }

    public IResult Add(Owner owner)
    {
        _ownerDal.Add(owner);
        return new SuccessResult(Messages.OwnerAdded);
    }

    public IResult Delete(int id) 
    { 
        var deletedOwner= _ownerDal.Get(o=> o.Id == id);
        deletedOwner.IsDeleted = true;
        _ownerDal.Update(deletedOwner);
        return new SuccessResult(Messages.OwnerDeleted);
    }

    public IResult Update(Owner owner)
    {
        _ownerDal.Update(owner);
        return new SuccessResult(Messages.OwnerUpdated);
    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_ownerDal.GetOwnersForSelectBox(), Messages.OwnersListed);

    }

    public IDataResult<List<Owner>> GetForTable()
    {
        return new SuccessDataResult<List<Owner>>(_ownerDal.GetForTable(), Messages.OwnersListed);

    }
}