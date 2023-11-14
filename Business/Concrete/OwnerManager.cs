using System.Runtime.InteropServices.JavaScript;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class OwnerManager: IOwnerService
{
    private readonly IOwnerDal _ownerDal;
    private readonly IVehicleDal _vehicleDal;

    public OwnerManager(IOwnerDal ownerDal, IVehicleDal vehicleDal)
    {
        _ownerDal = ownerDal;
        _vehicleDal = vehicleDal;
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
        
        var ownerExists = OwnerExists(owner.Name);
        if (ownerExists.Success)
        {
            return new ErrorResult(Messages.OwnerAlreadyExists);
        }
        _ownerDal.Add(owner);
        return new SuccessResult(Messages.OwnerAdded);
    }

    public IResult Delete(int id) 
    { 
        var result = CheckIfOwnerHasVehicles(id).Success;
        if (!result)
        {
            return new ErrorResult(Messages.OwnerHasVehicles);
        }
        var deletedOwner= _ownerDal.Get(o=> o.Id == id);

        deletedOwner.IsDeleted = true;
        _ownerDal.Update(deletedOwner);
        return new SuccessResult(Messages.OwnerDeleted);
    }

    public IResult Update(Owner owner)
    {
        var ownerExists = OwnerExists(owner.Name);
        if (ownerExists.Success)
        {
            return new ErrorResult(Messages.OwnerAlreadyExists);
        }
        _ownerDal.Update(owner);
        return new SuccessResult(Messages.OwnerUpdated);
    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_ownerDal.GetOwnersForSelectBox(), Messages.OwnersListed);

    }
    public IResult OwnerExists(string name)
    {
        if (_ownerDal.GetAll(o => o.Name == name &&  o.IsDeleted != true).Any())
        {
            return new SuccessResult(Messages.OwnerAlreadyExists);
        }
        return new ErrorResult();
    }
    
    

    public IDataResult<List<Owner>> GetForTable()
    {
        return new SuccessDataResult<List<Owner>>(_ownerDal.GetForTable(), Messages.OwnersListed);

    }
    
    private IResult CheckIfOwnerHasVehicles(int ownerId)
    {
        var result = _vehicleDal.GetAll(v => v.OwnerId == ownerId && v.IsDeleted != true);
        if (result.Any())
        {
            return new ErrorResult(Messages.OwnerHasVehicles);
        }

        return new SuccessResult();
    }
    
    
}