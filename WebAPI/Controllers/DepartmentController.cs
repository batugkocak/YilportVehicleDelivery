using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]s")]
[ApiController]
public class DepartmentController : Controller
{
    private IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _departmentService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{departmentId}")]
    public IActionResult Get(int departmentId)
    {
        var result = _departmentService.GetById(departmentId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Add(Department department)
    {
        var result = _departmentService.Add(department);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}