using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IDriverService
{
    IDataResult<List<Driver>> GetAll();
    IDataResult<Driver> GetById(int driverId);
    IResult Add(Driver driver);
    IResult Delete(Driver driver);
    IResult Update(Driver driver);
}