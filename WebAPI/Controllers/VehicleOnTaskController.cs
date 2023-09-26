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