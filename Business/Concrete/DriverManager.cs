using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

    public IResult Delete(int id)
    {
        var deletedDepartment= _driverDal.Get(o=> o.Id == id);
        deletedDepartment.IsDeleted = true;
        _driverDal.Update(deletedDepartment);
      return new SuccessResult(Messages.DriverDeleted);
    }

    public IResult Update(Driver driver)
    {
        _driverDal.Update(driver);
        return new SuccessResult(Messages.DriverUpdated);
    }

    public IDataResult<List<DriverDto>> GetForTable()
    {
        return new SuccessDataResult<List<DriverDto>>(_driverDal.GetForTable(), Messages.DriversListed);

    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_driverDal.GetDriversForSelectBox(), Messages.DriversListed);
    }
}