
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Task;
using Task = Entities.Concrete.Task;

namespace Business.Concrete;

public class TaskManager : ITaskService
{
    private ITaskDal _taskDal;

    public TaskManager(ITaskDal taskDal)
    {
        _taskDal = taskDal;
    }

    public IDataResult<List<Task>> GetAll()
    {
        return new SuccessDataResult<List<Task>>(_taskDal.GetAll(), Messages.TasksListed);
    }

    public IDataResult<List<TaskDetailDto>> GetForTable()
    {
        return new SuccessDataResult<List<TaskDetailDto>>(_taskDal.GetForTable(), Messages.TasksListed);

    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_taskDal.GetForSelectBox(), Messages.TasksListed);

    }


    public IDataResult<Task> GetById(int taskId)
    {
        return new SuccessDataResult<Task>(_taskDal.Get(d => d.Id == taskId), Messages.TaskListed);
    }

    public IResult Add(Task task)
    {
        _taskDal.Add(task);
        return new SuccessResult(Messages.TaskAdded);
    }
    

    public IResult Delete(int id)
    {
        var deletedTask= _taskDal.Get(o=> o.Id == id);
        deletedTask.IsDeleted = true;
        _taskDal.Update(deletedTask);
        return new SuccessResult(Messages.TaskDeleted);
    }

    public IResult Update(Task task)
    {
        _taskDal.Update(task);
        return new SuccessResult(Messages.TaskUpdated);
    }
}



