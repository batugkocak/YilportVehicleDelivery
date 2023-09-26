using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;


namespace Business.Abstract;

public interface IVehicleOnTaskService
{
    IDataResult<List<VehicleOnTask>> GetAll();
    IDataResult<VehicleOnTask> GetById(int taskId);
    IResult Add(VehicleOnTask vehicleOnTask);
    IResult Delete(VehicleOnTask vehicleOnTask);
    IResult Update(VehicleOnTask vehicleOnTask);
    
    //...
    public IResult FinishTask(int vehicleOnTaskId);
    public IDataResult<List<VehicleOnTaskDetailDto>> GetAllDetails();
}