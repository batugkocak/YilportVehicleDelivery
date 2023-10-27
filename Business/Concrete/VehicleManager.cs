using System.ComponentModel.DataAnnotations;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete;

//TODO: Change the plate XXX to XXX_ when deleted, because of "CheckIfCarExistByPlate()".
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
    
    public IResult Add(Vehicle vehicle)
    {
        ValidationTool.Validate(new VehicleValidator(), vehicle);
        
        var result = BusinessRules.Run(CheckIfCarExistByPlate(vehicle.Plate!));
        if (result != null)
        {
            return result;
        }
        _vehicleDal.Add(vehicle);
        return new SuccessResult(Messages.VehicleAdded);
    }

    public IResult Delete(Vehicle vehicle)
    {
        var result = BusinessRules.Run(CheckIfCarIsOnDuty(vehicle.Id));
        if (result != null)
        {
            return result;
        }

        var deletedCar= _vehicleDal.Get(v=> v.Id == vehicle.Id);
        deletedCar.IsDeleted = true;
        _vehicleDal.Update(deletedCar);
        return new SuccessResult(Messages.VehicleDeleted);
    }

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

    public IResult CheckIfCarExistByPlate(String plate)
    {
        var result = _vehicleDal.GetAll(u => u.Plate == plate).Any();
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

