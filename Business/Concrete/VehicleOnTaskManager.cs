using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
    
    public IDataResult<List<VehicleOnTaskDetailDto>> GetAllDetails()
    {
        return new SuccessDataResult<List<VehicleOnTaskDetailDto>>(_vehicleOnTaskDal.GetVehicleOnTaskDetail(), Messages.VehiclesOnTaskListed);
    }

    public IDataResult<VehicleOnTaskDetailDto> GetAllDetailsById(int id)
    {
        return new SuccessDataResult<VehicleOnTaskDetailDto>(_vehicleOnTaskDal.GetVehicleOnTaskDetailById(id), Messages.VehiclesOnTaskListed);

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

    
    public IResult FinishTask(int taskId)
    {
        var vehicleTask = _vehicleOnTaskDal.Get(t => t.Id == taskId);
        vehicleTask.ReturnDate = DateTime.Now;
        
         var updatedVehicle =  _vehicleService.GetById(vehicleTask.VehicleId).Data;
         updatedVehicle.Status = (int) VehicleStatus.Müsait;
         _vehicleService.Update(updatedVehicle);
        
        
        _vehicleOnTaskDal.Update(vehicleTask);
        return new SuccessResult(Messages.VehicleOnTaskTaskFinished);
    }
    
    public IResult DeleteTask(int taskId)
    {
        var vehicleTask = _vehicleOnTaskDal.Get(t => t.Id == taskId);
         vehicleTask.IsDeleted = true; //TODO: This might be isFinished or etc.
         
        _vehicleOnTaskDal.Update(vehicleTask);
        return new SuccessResult(Messages.VehicleOnTaskTaskDeleted);
    }
    
    private IResult CheckIfVehicleOnDutyById(int vehicleId)
    {
        var result = _vehicleOnTaskDal.GetAll(v=>v.VehicleId == vehicleId && v.ReturnDate == DateTime.Parse("1.01.0001 00:00:00")).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyOnTask);
        }

        return new SuccessResult();
    }
}