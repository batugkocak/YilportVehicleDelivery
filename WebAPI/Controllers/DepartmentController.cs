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
    [Route("ForSelectBox")]
    public IActionResult GetForSelectBox()
    {
        var result = _departmentService.GetForSelectBox();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("ForTable")]
    public IActionResult GetForTable()
    {
        var result = _departmentService.GetForTable();
        return Ok(result);
    }
    
    [HttpGet("{departmentId}")]
    public IActionResult Get(int departmentId)
    {
        var result = _departmentService.GetById(departmentId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(Department department)
    {
        var result = _departmentService.Add(department);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Delete/{departmentId}")]
    [HttpPost]
    public IActionResult Delete(int departmentId)
    {
        var result = _departmentService.Delete(departmentId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Update")]
    [HttpPost]
    public IActionResult Update(Department department)
    {
        var result = _departmentService.Update(department);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}