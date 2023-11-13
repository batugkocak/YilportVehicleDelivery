using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Department;

namespace Business.Concrete;

public class DepartmentManager : IDepartmentService
{
    private IDepartmentDal _departmentDal;

    public DepartmentManager(IDepartmentDal departmentDal)
    {
        _departmentDal = departmentDal;
    }
    
    public IDataResult<List<Department>> GetAll()
    {
        return new SuccessDataResult<List<Department>>(_departmentDal.GetAll(), Messages.DepartmentsListed);
    }
    
    public IDataResult<Department> GetById(int departmentId)
    {
        return new SuccessDataResult<Department>(_departmentDal.Get(d => d.Id == departmentId));
    }
    
    public IResult Add(Department department)
    {
        _departmentDal.Add(department);
        return new SuccessResult(Messages.DepartmentsAdded);
    }
    
    public IResult Delete(int id)
    {
        var deletedDepartment= _departmentDal.Get(o=> o.Id == id);
        deletedDepartment.IsDeleted = true;
        _departmentDal.Update(deletedDepartment);
        return new SuccessResult(Messages.DepartmentDeleted);
    }
    
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
}