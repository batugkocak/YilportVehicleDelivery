using Core.Utilities.Results;
using Entities.Concrete;
using Task = Entities.Concrete.Task;

namespace Business.Abstract;

public interface ITaskService
{
    IDataResult<List<Task>> GetAll();
    IDataResult<Task> GetById(int taskId);
    IResult Add(Task task);
    IResult Delete(Task task);
    IResult Update(Task task);
}