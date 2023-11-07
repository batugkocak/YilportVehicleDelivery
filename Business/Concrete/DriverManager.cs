using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Driver;

namespace Business.Concrete;

public class DriverManager: IDriverService
{
    private IDriverDal _driverDal;

    public DriverManager(IDriverDal driverDal)
    {
        _driverDal = driverDal;
    }
    public IDataResult<List<Driver>> GetAll()
    {
        return new SuccessDataResult<List<Driver>>(_driverDal.GetAll(), Messages.DriversListed);
    }

    public IDataResult<Driver> GetById(int driverId)
    {
        return new SuccessDataResult<Driver>(_driverDal.Get(d => d.Id == driverId), Messages.DriverListed);
    }

    public IResult Add(Driver driver)
    {
        _driverDal.Add(driver);
        return new SuccessResult(Messages.DriverAdded);
    }

    public IResult Delete(Driver driver)
    {
      _driverDal.Delete(driver);
      return new SuccessResult(Messages.DriverDeleted);
    }

    public IResult Update(Driver driver)
    {
        _driverDal.Update(driver);
        return new SuccessResult(Messages.DriverUpdated);
    }

    public IDataResult<List<DriverDto>> GetAllDetails()
    {
        return new SuccessDataResult<List<DriverDto>>(_driverDal.GetDriverDetails(), Messages.DriversListed);

    }
}