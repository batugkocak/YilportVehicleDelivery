
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

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
    
    public IDataResult<List<VehicleForTableDto>> GetAllDetails()
    {
        return new SuccessDataResult<List<VehicleForTableDto>>(_vehicleDal.GetVehicleDetails(), Messages.VehiclesListed);
    }

    public IDataResult<Vehicle> GetById(int vehicleId)
    {
        return new SuccessDataResult<Vehicle>(_vehicleDal.Get(v => v.Id == vehicleId));
    }
    
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

    public IResult Delete(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Vehicle vehicle)
    {
        _vehicleDal.Update(vehicle);
        return new SuccessResult(Messages.VehicleUpdated);
    }
    
    //
    private IResult CheckIfCarExistByPlate(String plate)
    {
        var result = _vehicleDal.GetAll(u => u.Plate == plate).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyExist);
        }

        return new SuccessResult();
    }
}