using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;


public interface IVehicleService
{
    IDataResult<List<Vehicle>> GetAll();
    IDataResult<Vehicle> GetById(int vehicleId);
    IResult Add(Vehicle vehicle);
    IResult Delete(Vehicle vehicle);
    IResult Update(Vehicle vehicle);
}