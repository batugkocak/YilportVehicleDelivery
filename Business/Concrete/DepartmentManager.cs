using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

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
        return new SuccessDataResult<List<Department>>(_departmentDal.GetAll().ToList(), Messages.DepartmentsListed);
    }

    public IDataResult<Department> GetById(int departmentId)
    {
        return new SuccessDataResult<Department>(_departmentDal.Get(d => d.Id == departmentId));
    }

    public IResult Add(Department department)
    {
        _departmentDal.Add(department);
        return new SuccessResult();
    }

    public IResult Delete(Department department)
    {
        _departmentDal.Delete(department);
        return new SuccessResult();
    }

    public IResult Update(Department department)
    {
        _departmentDal.Update(department);
        return new SuccessResult();
    }
}