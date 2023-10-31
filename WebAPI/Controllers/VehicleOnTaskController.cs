using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]s")]
[ApiController]
public class VehicleOnTaskController : Controller
{
    private IVehicleOnTaskService _vehicleOnTaskService;

    public VehicleOnTaskController(IVehicleOnTaskService vehicleOnTaskService)
    {
        _vehicleOnTaskService = vehicleOnTaskService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _vehicleOnTaskService.GetAll();
        return Ok(result);
    }
    [Route("Details")]
    [HttpGet]
    public IActionResult GetDetailed()
    {
        var result = _vehicleOnTaskService.GetAllDetails();
        return Ok(result);
    }
    
    [Route("ForNormalTable")]
    [HttpGet]
    public IActionResult GetForNormalTable()
    {
        var result = _vehicleOnTaskService.GetAllForTable(false);
        return Ok(result);
    }
    
    [Route("ForArchiveTable")]
    [HttpGet]
    public IActionResult GetForArchiveTable()
    {
        var result = _vehicleOnTaskService.GetAllForTable(true);
        return Ok(result);
    }
    
    [Route("Details/{id}")]
    [HttpGet]
    public IActionResult GetDetailedById(int id)
    {
        var result = _vehicleOnTaskService.GetAllDetailsById(id);
        return Ok(result);
    }
    
    [HttpGet("{vehicleOnTaskId}")]
    public IActionResult Get(int vehicleOnTaskId)
    {
        var result = _vehicleOnTaskService.GetById(vehicleOnTaskId);
        return Ok(result);
    }
    
    // [HttpPatch("finishTask/{vehicleOnTaskId}")]
    // public IActionResult FinishTask(int vehicleOnTaskId)
    // {
    //     var result = _vehicleOnTaskService.FinishTask(vehicleOnTaskId);
    //     
    //     return Ok(result);
    // }
    
    [Route("Finish/{taskId}")]
    [HttpPost]
    public IActionResult FinishTask(int taskId)
    {
        var result = _vehicleOnTaskService.FinishTask(taskId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Update")]
    [HttpPost]
    public IActionResult Update(VehicleOnTask vehicleOnTask)
    {
        var result = _vehicleOnTaskService.Update(vehicleOnTask);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Delete/{taskId}")]
    [HttpPost]
    public IActionResult DeleteTask(int taskId)
    {
        var result = _vehicleOnTaskService.DeleteTask(taskId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Add(VehicleOnTask vehicleOnTask)
    {
        var result = _vehicleOnTaskService.Add(vehicleOnTask);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}

/*
{
   "id": 68,
   "vehicleId": 1,
   "driverId": 1,
   "departmentId": 1,
   "authorizedPerson": "string",
   "address": "Bursa",
   "taskDefinition": "Bakım",
   "givenDate": "2023-10-03T11:46:53",
   "returnDate": "0001-01-01T00:00:00"
   }
*/