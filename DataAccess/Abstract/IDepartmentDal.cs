using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IDepartmentDal : IEntityRepository<Department>
{
    public List<SelectBoxDto> GetDepartmentsForSelectBox();
}