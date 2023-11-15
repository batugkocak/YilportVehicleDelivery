using System.ComponentModel.DataAnnotations;
using Business.Abstract;
using Business.Aspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete;
public class VehicleManager : IVehicleService
{
    private IVehicleDal _vehicleDal;

    public VehicleManager(IVehicleDal vehicleDal)
    {
        _vehicleDal = vehicleDal;
    }
    
    public IDataResult<List<Vehicle>> GetAll()
    {
        return new SuccessDataResult<List<Vehicle>>(_vehicleDal.GetAll(), Messages.VehiclesListed);
    }
    
    
    public IDataResult<List<VehicleForTableDTO>> GetAllDetailsForTable()
    {
        var result = _vehicleDal.GetVehicleDetails();
        if (result != null)
        {
            return new SuccessDataResult<List<VehicleForTableDTO>>(result, Messages.VehiclesListed);

        }
        return new ErrorDataResult<List<VehicleForTableDTO>>(null, "Araç Bulunamadı.");
    }

    public IDataResult<VehicleDetailDTO> GetDetailsById(int vehicleId)
    {
        var result = _vehicleDal.GetVehicleDetailsById(vehicleId);
        if (result != null)
        {
            return new SuccessDataResult<VehicleDetailDTO>(result, Messages.VehiclesListed);
        }
        return new ErrorDataResult<VehicleDetailDTO>(null, Messages.NotFound);
    }

    public IDataResult<Vehicle> GetById(int vehicleId)
    {
        var result = _vehicleDal.Get(v => v.Id == vehicleId);
        if (result != null)
        {
            return new SuccessDataResult<Vehicle>(result, Messages.VehicleListed);
        }
        return new ErrorDataResult<Vehicle>(null, Messages.NotFound);
    }
    
    
    public IDataResult<Vehicle> GetByPlate(string plate)
    {
        var result = _vehicleDal.Get(v => v.Plate == plate);
        if (result != null)
        {
            return new SuccessDataResult<Vehicle>(result, Messages.VehicleListed);
        }
        return new ErrorDataResult<Vehicle>(null, Messages.NotFound);
    }
    
    [SecuredOperation("admin,user")]
    [ValidationAspect(typeof(VehicleValidator))]
    public IResult Add(Vehicle vehicle)
    {
        var result = BusinessRules.Run(CheckIfCarExistByPlate(vehicle.Plate!));
        if (result != null)
        {
            return result;
        }
        _vehicleDal.Add(vehicle);
        return new SuccessResult(Messages.VehicleAdded);
    }

    public IResult Delete(int id)
    {
        var result = BusinessRules.Run(CheckIfCarIsOnDuty(id));
        if (result != null)
        {
            return result;
        }

        var deletedCar= _vehicleDal.Get(v=> v.Id == id);
        deletedCar.IsDeleted = true;
        _vehicleDal.Update(deletedCar);
        return new SuccessResult(Messages.VehicleDeleted);
    }
    
    [ValidationAspect(typeof(VehicleValidator))]
    public IResult Update(Vehicle vehicle)
    {
        var oldVehicle = _vehicleDal.Get(v => v.Id == vehicle.Id);
        if (oldVehicle.Plate != vehicle.Plate)
        {
            var result = BusinessRules.Run(CheckIfCarExistByPlate(vehicle.Plate!));
            if (result != null)
            {
                return result;
            } 
        }
        _vehicleDal.Update(vehicle);
        return new SuccessResult(Messages.VehicleUpdated);
    }
    
    [SecuredOperation("admin,user")]
    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_vehicleDal.GetVehiclesForSelectBox(), Messages.VehiclesListed);
    }

    public IResult CheckIfCarExistByPlate(String plate)
    {
        var result = _vehicleDal.GetAll(u => u.Plate == plate && u.IsDeleted != true).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyExist);
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfCarIsOnDuty(int id)
    {
        var result = _vehicleDal.Get(v => v.Id == id).Status == (int) VehicleStatus.Görevde;
        if (result)
        {
            return new ErrorResult(Messages.VehicleIsOnDuty);
        }

        return new SuccessResult();
    }
}

