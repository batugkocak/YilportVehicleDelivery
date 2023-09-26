using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("[controller]s")]
[ApiController]
public class DriverController : Controller
{
    private IDriverService _driverService;

    public DriverController(IDriverService driverService)
    {
        _driverService = driverService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var result = _driverService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{driverId}")]
    public IActionResult Get(int driverId)
    {
        var result = _driverService.GetById(driverId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Add(Driver driver)
    {
        var result = _driverService.Add(driver);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}
