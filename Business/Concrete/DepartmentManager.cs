using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Department;

namespace Business.Concrete;

public class DepartmentManager : IDepartmentService
{
    private IDepartmentDal _departmentDal;
    private IVehicleService _vehicleService;
    private IVehicleOnTaskService _vehicleOnTaskService;
    private ITaskService _taskService;
    

    public DepartmentManager(IDepartmentDal departmentDal, IVehicleService vehicleService, IVehicleOnTaskService vehicleOnTaskService, ITaskService taskService )
    {
        _departmentDal = departmentDal;
        _vehicleService = vehicleService;
        _vehicleOnTaskService = vehicleOnTaskService;
        _taskService = taskService;
    }
    
    public IDataResult<List<Department>> GetAll()
    {
        return new SuccessDataResult<List<Department>>(_departmentDal.GetAll(), Messages.DepartmentsListed);
    }
    
    public IDataResult<Department> GetById(int departmentId)
    {
        return new SuccessDataResult<Department>(_departmentDal.Get(d => d.Id == departmentId));
    }

    [ValidationAspect(typeof(DepartmentValidator))]
    public IResult Add(Department department)
    {
        _departmentDal.Add(department);
        return new SuccessResult(Messages.DepartmentsAdded);
    }
    
    public IResult Delete(int id)
    {
          var result = CheckIfDepartmentHasVehicles(id);
          if (!result.Success)
          {
              return new ErrorResult(result.Message);
          }
        
          result = CheckIfDepartmentHasActiveTask(id);
         if (!result.Success)
         {
             return new ErrorResult(result.Message);
         }
        
         result = CheckIfDepartmentHasPredefinedTask(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }
        
        var deletedDepartment= _departmentDal.Get(o=> o.Id == id);
        deletedDepartment.IsDeleted = true;
        _departmentDal.Update(deletedDepartment);
        return new SuccessResult(Messages.DepartmentDeleted);
    }

    [ValidationAspect(typeof(DepartmentValidator))]
    public IResult Update(Department department)
    {
        _departmentDal.Update(department);
        return new SuccessResult(Messages.DepartmentUpdated);
    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_departmentDal.GetDepartmentsForSelectBox(), Messages.DepartmentsListed);
    }

    public IDataResult<List<DepartmentForTableDto>> GetForTable()
    {
        return new SuccessDataResult<List<DepartmentForTableDto>>(_departmentDal.GetForTable(), Messages.DepartmentsListed);

    }
    
    private IResult CheckIfDepartmentHasVehicles(int departmentId)
    {
        var result = _vehicleService.GetByDepartmentId(departmentId);
        if (result.Data.Any())
        {
            return new ErrorResult(Messages.DepartmentHasVehicles);
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfDepartmentHasActiveTask(int departmentId)
    {
        var result = _vehicleOnTaskService.GetByDepartmentId(departmentId);
        if (result.Data.Any())
        {
            return new ErrorResult(Messages.DepartmentHasActiveTask);
        }

        return new SuccessResult();
    }
    
    private IResult CheckIfDepartmentHasPredefinedTask(int departmentId)
    {
        var result = _taskService.GetByDepartmentId(departmentId);
        if (result.Data.Any())
        {
            return new ErrorResult(Messages.DepartmantHasPredefinedTask);
        }

        return new SuccessResult();
    }
}