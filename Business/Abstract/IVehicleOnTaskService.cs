using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;


namespace Business.Abstract;

public interface IVehicleOnTaskService
{
    IDataResult<List<VehicleOnTask>> GetAll();
    IDataResult<VehicleOnTask> GetById(int taskId);
    IResult Add(VehicleOnTask vehicleOnTask);
    
    IResult Update(VehicleOnTask vehicleOnTask);
    
    
    public IResult DeleteTask(int taskId);

    
    public IResult FinishTask(int vehicleOnTaskId);
    public IDataResult<List<VehicleOnTaskDetailDto>> GetAllDetails();

    public IDataResult<VehicleOnTaskDetailDto> GetAllDetailsById(int id);
    public IDataResult<List<VehicleOnTaskForTableDto>> GetAllForTable(bool isFinished);

}