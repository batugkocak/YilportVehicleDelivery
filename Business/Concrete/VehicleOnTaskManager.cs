using Business.Abstract;
using Business.Aspects.Autofac;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.FilterQueryObjects.VehicleOnTask;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class VehicleOnTaskManager: IVehicleOnTaskService
{
    private IVehicleOnTaskDal _vehicleOnTaskDal;
    private IVehicleService _vehicleService;

    public VehicleOnTaskManager(IVehicleOnTaskDal vehicleOnTaskDal, IVehicleService vehicleService)
    {
        _vehicleOnTaskDal = vehicleOnTaskDal;
        _vehicleService = vehicleService;
    }

    public IDataResult<List<VehicleOnTask>> GetAll()
    {
        return new SuccessDataResult<List<VehicleOnTask>>(_vehicleOnTaskDal.GetAll(vot => vot.IsDeleted != true), Messages.VehiclesOnTaskListed);
    }
    
    public IDataResult<List<VehicleOnTaskDetailDto>> GetAllDetails(VotFilterRequest filterRequest)
    {
        if (filterRequest.FirstDate == null) filterRequest.FirstDate = DateTime.MinValue;
        if (filterRequest.LastDate == null) filterRequest.LastDate = DateTime.Now;
        if (filterRequest.Size == -1) filterRequest.Size = int.MaxValue;
        return new SuccessDataResult<List<VehicleOnTaskDetailDto>>(_vehicleOnTaskDal.GetVehicleOnTaskDetailFinished(filterRequest), Messages.VehiclesOnTaskListed);
    }

    public IDataResult<VehicleOnTaskDetailDto> GetAllDetailsById(int id)
    {
        return new SuccessDataResult<VehicleOnTaskDetailDto>(_vehicleOnTaskDal.GetVehicleOnTaskDetailById(id), Messages.VehicleOnTaskListed);

    }

    
    
    public IDataResult<PagingResponse<VehicleOnTaskForTableDto>> GetAllForArchiveTable(VotFilterRequest filterRequest)
    {
        if (filterRequest.FirstDate == null) filterRequest.FirstDate = DateTime.MinValue;
        if (filterRequest.LastDate == null) filterRequest.LastDate = DateTime.Now;
        if (filterRequest.Size == -1) filterRequest.Size = int.MaxValue;

        var result = _vehicleOnTaskDal.GetVehicleOnTaskForTableFinished(filterRequest);
        if (result != null)
        {
            foreach (var vot in result.Items)
            {
                vot.GivenDate = vot.GivenDate;
            }
            return new SuccessDataResult<PagingResponse<VehicleOnTaskForTableDto>>(
                _vehicleOnTaskDal.GetVehicleOnTaskForTableFinished(filterRequest),
                Messages.VehiclesOnTaskListed);

        }
        return new ErrorDataResult<PagingResponse<VehicleOnTaskForTableDto>>(null, "Araç Bulunamadı.");
    }
     public IDataResult<List<VehicleOnTaskForTableDto>> GetAllForTable()
    {
        return new SuccessDataResult<List<VehicleOnTaskForTableDto>>(_vehicleOnTaskDal.GetVehicleOnTaskForTable(),
            Messages.VehiclesOnTaskListed);
    }


    public IDataResult<VehicleOnTask> GetById(int taskId)
    {
        return new SuccessDataResult<VehicleOnTask>(_vehicleOnTaskDal.Get(vt => vt.Id == taskId), Messages.VehicleOnTaskListed);
    }

    [SecuredOperation("admin,user")]
    public IResult Add(VehicleOnTask vehicleOnTask)
    {
        var result = BusinessRules.Run(CheckIfVehicleOnDutyById(vehicleOnTask.VehicleId));
        if (result != null)
        {
            return result;
        }
        vehicleOnTask.GivenDate = DateTime.Now;
        
       var updatedVehicle =  _vehicleService.GetById(vehicleOnTask.VehicleId).Data;
       updatedVehicle.Status = (int) VehicleStatus.Görevde;
       _vehicleService.Update(updatedVehicle);

        _vehicleOnTaskDal.Add(vehicleOnTask);
        return new SuccessResult(Messages.VehicleOnTaskAdded);
    }

    [SecuredOperation("admin,user")]
    public IResult Update(VehicleOnTask vehicleOnTask)
    {
        var oldVehicleOnTask = _vehicleOnTaskDal.Get(v => v.Id == vehicleOnTask.Id);
        if (oldVehicleOnTask.VehicleId != vehicleOnTask.VehicleId)
        {
            var result = BusinessRules.Run(CheckIfVehicleOnDutyById(vehicleOnTask.VehicleId));
            if (result != null)
            {
                return result;
            }
        }
        _vehicleOnTaskDal.Update(vehicleOnTask);
        return new SuccessResult(Messages.VehicleOnTaskUpdated);
    }

    [SecuredOperation("admin,user")]
    public IResult FinishTask(int taskId)
    {
        var vehicleTask = _vehicleOnTaskDal.Get(t => t.Id == taskId);
        vehicleTask.ReturnDate = DateTime.Now;
        vehicleTask.IsFinished = true;
        MakeCarAvailable(vehicleTask);
        
        _vehicleOnTaskDal.Update(vehicleTask);
        return new SuccessResult(Messages.VehicleOnTaskTaskFinished);
    }
    
    [SecuredOperation("admin,user")]
    public IResult DeleteTask(int taskId)
    {
        var vehicleTask = _vehicleOnTaskDal.Get(t => t.Id == taskId);
         vehicleTask.IsDeleted = true;
         
        MakeCarAvailable(vehicleTask);
         
        _vehicleOnTaskDal.Update(vehicleTask);
        return new SuccessResult(Messages.VehicleOnTaskTaskDeleted);
    }
    
    private IResult CheckIfVehicleOnDutyById(int vehicleId)
    {
        var result = _vehicleOnTaskDal.GetAll(v=>v.VehicleId == vehicleId && v.IsFinished != true && v.IsDeleted != true).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyOnTask);
        }

        return new SuccessResult();
    }

    private void MakeCarAvailable(VehicleOnTask vehicleOnTask)
    {
        var updatedVehicle =  _vehicleService.GetById(vehicleOnTask.VehicleId).Data;
        updatedVehicle.Status = (int) VehicleStatus.Müsait;
        _vehicleService.Update(updatedVehicle);
    }
}