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
        return new SuccessDataResult<List<VehicleOnTask>>(_vehicleOnTaskDal.GetAll(vod => vod.IsDeleted != true), Messages.VehiclesOnTaskListed);
    }
    
    public IDataResult<List<VehicleOnTaskDetailDto>> GetAllDetails()
    {
        return new SuccessDataResult<List<VehicleOnTaskDetailDto>>(_vehicleOnTaskDal.GetVehicleOnTaskDetailDal(), Messages.VehiclesOnTaskListed);
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

        //TODO: This needs 1 API GET request. Find something optimized to update car status.
        
       var updatedVehicle =  _vehicleService.GetById(vehicleOnTask.VehicleId).Data;
       updatedVehicle.Status = 2;
       _vehicleService.Update(updatedVehicle);

        _vehicleOnTaskDal.Add(vehicleOnTask);
        return new SuccessResult(Messages.VehicleOnTaskAdded);
    }

    public IResult Delete(VehicleOnTask vehicleOnTask)
    {
        // vehicleOnTask.ReturnDate = DateTime.Now;
        // vehicleOnTask.IsDeleted = true;
        //
        // var updatedVehicle =  _vehicleService.GetById(vehicleOnTask.VehicleId).Data;
        // updatedVehicle.Status = 1;
        // _vehicleService.Update(updatedVehicle);
        //
        // _vehicleOnTaskDal.Update(vehicleOnTask);
        //TODO: 
        return new SuccessResult(Messages.VehicleOnTaskDeleted);
    }

    public IResult Update(VehicleOnTask vehicleOnTask)
    {
        _vehicleOnTaskDal.Update(vehicleOnTask);
        return new SuccessResult(Messages.VehicleOnTaskUpdated);
    }
    
    public IResult FinishTask(int taskId)
    {
        var vehicleTask = _vehicleOnTaskDal.Get(t => t.Id == taskId);
        vehicleTask.ReturnDate = DateTime.Now;
        vehicleTask.IsDeleted = true; //TODO: This might be isFinished or etc.
        
         var updatedVehicle =  _vehicleService.GetById(vehicleTask.VehicleId).Data;
         updatedVehicle.Status = (int) VehicleStatus.Görevde;
         _vehicleService.Update(updatedVehicle);
        
        
        _vehicleOnTaskDal.Update(vehicleTask);
        return new SuccessResult(Messages.VehicleOnTaskUpdated);
    }
    
    private IResult CheckIfVehicleOnDutyById(int vehicleId)
    {
        var result = _vehicleOnTaskDal.GetAll(v=>v.VehicleId == vehicleId && v.IsDeleted == false).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyOnTask);
        }

        return new SuccessResult();
    }
}