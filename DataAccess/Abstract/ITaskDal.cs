using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Task;
using Task = Entities.Concrete.Task;

namespace DataAccess.Abstract;

public interface ITaskDal : IEntityRepository<Task>
{
    public List<TaskDetailDto> GetForTable();
    public List<SelectBoxDto> GetForSelectBox();
}