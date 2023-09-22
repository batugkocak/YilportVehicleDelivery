using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IDepartmentService
{
    IDataResult<Department> GetById(int departmentId);

}