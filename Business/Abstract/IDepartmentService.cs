using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IDepartmentService
{
    IDataResult<List<Department>> GetAll();
    IDataResult<Department> GetById(int brandId);
    IResult Add(Department brand);
    IResult Delete(Department brand);
    IResult Update(Department brand);

}