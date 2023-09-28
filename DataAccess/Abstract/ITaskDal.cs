using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Task = Entities.Concrete.Task;

namespace DataAccess.Abstract;

public interface ITaskDal : IEntityRepository<Task>
{
    public List<TaskDetailDto> GetTaskDetails();
}