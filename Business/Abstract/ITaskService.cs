using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Task;
using Task = Entities.Concrete.Task;

namespace Business.Abstract;

public interface ITaskService
{
    IDataResult<List<Task>> GetAll();
    IDataResult<Task> GetById(int taskId);
    IResult Add(Task task);
    IResult Delete(int id);
    IResult Update(Task task);

    IDataResult<List<TaskDetailDto>> GetForTable();
    IDataResult<List<SelectBoxDto>> GetForSelectBox();

}