using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Driver;

namespace Business.Abstract;

public interface IDriverService
{
    IDataResult<List<Driver>> GetAll();
    IDataResult<Driver> GetById(int driverId);
    IResult Add(Driver driver);
    IResult Delete(Driver driver);
    IResult Update(Driver driver);
    
    IDataResult<List<DriverDto>> GetAllDetails();
}