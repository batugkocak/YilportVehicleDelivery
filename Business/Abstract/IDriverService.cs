using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Driver;

namespace Business.Abstract;

public interface IDriverService
{
    IDataResult<List<Driver>> GetAll();
    IDataResult<Driver> GetById(int driverId);
    IResult Add(Driver driver);
    IResult Delete(int id);
    IResult Update(Driver driver);
    
    IDataResult<List<DriverDto>> GetForTable();

    IDataResult<List<SelectBoxDto>> GetForSelectBox();
}