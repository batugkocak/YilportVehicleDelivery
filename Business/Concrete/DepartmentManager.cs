using Business.Abstract;
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
    public IDataResult<Department> GetById(int departmentId)
    {
        return new SuccessDataResult<Department>(_departmentDal.Get(d => d.Id == departmentId));

    }
}