using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.FilterQueryObjects.VehicleOnTask;
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
    public IDataResult<PagingResponse<VehicleOnTaskForTableDto>> GetAllForArchiveTable(VotFilterRequest filterRequest);
    public IDataResult<List<VehicleOnTaskForTableDto>> GetAllForTable( );

}