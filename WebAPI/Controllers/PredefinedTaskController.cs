using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Task = Entities.Concrete.Task;

namespace WebAPI.Controllers;


[Route("[controller]s")]
[ApiController]
public class PredefinedTaskController : Controller
{
    private readonly ITaskService _taskService;

    public PredefinedTaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var result = _taskService.GetAll();
        return Ok(result);
    }
    
    [Route("Details")]
    [HttpGet]
    public IActionResult GetDetailed()
    {
        var result = _taskService.GetAllDetails();
        return Ok(result);
    }
    
    [HttpGet("{taskId}")]
    public IActionResult Get(int taskId)
    {
        var result = _taskService.GetById(taskId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(Task task)
    {
        var result = _taskService.Add(task);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}