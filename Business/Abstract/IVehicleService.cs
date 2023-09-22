using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;


public interface IVehicleService
{
    IDataResult<List<VehicleDetailsDto>> GetAll();
    IDataResult<Vehicle> GetById(int vehicleId);
    IResult Add(Vehicle vehicle);
    IResult Delete(Vehicle vehicle);
    IResult Update(Vehicle vehicle);
}