using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfDepartmentDal : EfEntityRepositoryBase<Department, VehicleDeliveryContext>, IDepartmentDal
{
    public List<SelectBoxDto> GetDepartmentsForSelectBox()
    {
        using VehicleDeliveryContext context = new();
        var result =  (from department in context.Departments
            where department.IsDeleted != true
            select new SelectBoxDto()
            {
                Id = department.Id,
                SelectBoxValue = department.Name,
            }).ToList();

        return result;
    }
}