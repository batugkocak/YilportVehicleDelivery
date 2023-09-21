using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

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
        return new SuccessDataResult<List<Vehicle>>(_vehicleDal.GetAll());
    }

    public IDataResult<Vehicle> GetById(int vehicleId)
    {
        return new SuccessDataResult<Vehicle>(_vehicleDal.Get(v => v.Id == vehicleId));
    }

    
    public IResult Add(Vehicle vehicle)
    {
        _vehicleDal.Add(vehicle);
        return new SuccessResult(Messages.VehicleAdded);
    }

    public IResult Delete(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }
}