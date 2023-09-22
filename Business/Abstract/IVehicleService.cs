using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;


public interface IVehicleService
{
    IDataResult<List<VehicleDetailDto>> GetAll();
    
    // IDataResult<List<VehicleDetailDto>> GetAllWithoutService();

    IDataResult<Vehicle> GetById(int vehicleId);
    IResult Add(Vehicle vehicle);
    IResult Delete(Vehicle vehicle);
    IResult Update(Vehicle vehicle);
}