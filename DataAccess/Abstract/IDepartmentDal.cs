using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Department;

namespace DataAccess.Abstract;

public interface IDepartmentDal : IEntityRepository<Department>
{
    public List<SelectBoxDto> GetDepartmentsForSelectBox();
    public List<DepartmentForTableDto> GetForTable();
}