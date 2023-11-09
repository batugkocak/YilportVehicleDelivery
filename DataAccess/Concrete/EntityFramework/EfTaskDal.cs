using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.DTOs;
using Entities.DTOs.Task;
using Task = Entities.Concrete.Task;

namespace DataAccess.Concrete.EntityFramework;

public class EfTaskDal: EfEntityRepositoryBase<Task, VehicleDeliveryContext>, ITaskDal
{
    public List<TaskDetailDto> GetForTable()
    {
        using VehicleDeliveryContext context = new VehicleDeliveryContext();
        var result = (from t in context.Tasks
            join d in context.Departments on t.DepartmentId equals d.Id
            orderby t.Created descending 
            where t.IsDeleted != true
            select new TaskDetailDto()
            {
                Id = t.Id,
                Name = t.Name,
                DepartmentName = d.Name,
                Address = t.Address
            }).ToList();

        return result;
    }
    public List<TaskDetailDto> GetDetails()
    {
        using VehicleDeliveryContext context = new VehicleDeliveryContext();
        var result = (from t in context.Tasks
            join d in context.Departments on t.DepartmentId equals d.Id
            orderby t.Created descending 
            select new TaskDetailDto()
            {
                Id = t.Id,
                Name = t.Name,
                DepartmentName = d.Name,
                Address = t.Address
            }).ToList();

        return result;
    }
    
    public List<SelectBoxDto> GetForSelectBox()
    {
        using VehicleDeliveryContext context = new();
        var result =  (from task in context.Tasks
            where task.IsDeleted != true
            select new SelectBoxDto()
            {
                Id = task.Id,
                SelectBoxValue = task.Name
            }).ToList();

        return result;
    }
}