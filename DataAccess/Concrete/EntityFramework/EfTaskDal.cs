using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Task = Entities.Concrete.Task;

namespace DataAccess.Concrete.EntityFramework;

public class EfTaskDal: EfEntityRepositoryBase<Task, VehicleDeliveryContext>, ITaskDal
{
    
}