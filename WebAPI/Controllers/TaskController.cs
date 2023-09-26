using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[Route("[controller]s")]
[ApiController]
public class TaskController : Controller
{
    private IVehicleOnTaskService _vehicleOnTaskService;

    public TaskController(IVehicleOnTaskService vehicleOnTaskService)
    {
        _vehicleOnTaskService = vehicleOnTaskService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var result = _vehicleOnTaskService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{taskId}")]
    public IActionResult Get(int taskId)
    {
        var result = _vehicleOnTaskService.GetById(taskId);
        return Ok(result);
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