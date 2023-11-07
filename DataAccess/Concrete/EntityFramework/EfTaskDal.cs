using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.DTOs.Task;
using Task = Entities.Concrete.Task;

namespace DataAccess.Concrete.EntityFramework;

public class EfTaskDal: EfEntityRepositoryBase<Task, VehicleDeliveryContext>, ITaskDal
{
    public List<TaskDetailDto> GetTaskDetails()
    {
        using VehicleDeliveryContext context = new VehicleDeliveryContext();
        var result = (from t in context.Tasks
            join d in context.Departments on t.DepartmentId equals d.Id
            select new TaskDetailDto()
            {
                Name = t.Name,
                DepartmentName = d.Name,
                Address = t.Address
            }).ToList();

        return result;
    }
}