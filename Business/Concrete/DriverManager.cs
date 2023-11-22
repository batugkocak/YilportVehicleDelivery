using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Driver;

namespace Business.Concrete;

public class DriverManager: IDriverService
{
    private IDriverDal _driverDal;
    private IVehicleOnTaskService _vehicleOnTaskService;

    public DriverManager(IDriverDal driverDal, IVehicleOnTaskService vehicleOnTaskService)
    {
        _driverDal = driverDal;
        _vehicleOnTaskService = vehicleOnTaskService;
    }
    public IDataResult<List<Driver>> GetAll()
    {
        return new SuccessDataResult<List<Driver>>(_driverDal.GetAll(), Messages.DriversListed);
    }

    public IDataResult<Driver> GetById(int driverId)
    {
        return new SuccessDataResult<Driver>(_driverDal.Get(d => d.Id == driverId), Messages.DriverListed);
    }

    [ValidationAspect(typeof(DriverValidator))]
    public IResult Add(Driver driver)
    {
        _driverDal.Add(driver);
        return new SuccessResult(Messages.DriverAdded);
    }

    public IResult Delete(int id)
    {
        
        var result = CheckIfDriverIsOnMission(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }
        
        var deletedDriver= _driverDal.Get(d=> d.Id == id);
        deletedDriver.IsDeleted = true;
        _driverDal.Update(deletedDriver);
      return new SuccessResult(Messages.DriverDeleted);
    }

    [ValidationAspect(typeof(DriverValidator))]
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
    
    private IResult CheckIfDriverIsOnMission(int driverId)
    {
        var result = _vehicleOnTaskService.GetByDriverId(driverId);
         if (result.Data.Any())
         {
             return new ErrorResult(Messages.DriverIsOnMission);
         }

        return new SuccessResult();
    }
}