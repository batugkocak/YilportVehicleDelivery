using Core.DataAccess;
using Entities.Concrete;
using Task = Entities.Concrete.Task;

namespace DataAccess.Abstract;

public interface ITaskDal : IEntityRepository<Task>
{
    
}