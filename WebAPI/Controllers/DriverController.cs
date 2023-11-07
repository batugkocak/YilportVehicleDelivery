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
    [Route("Details")]
    public IActionResult Get()
    {
        var result = _driverService.GetAllDetails();
        return Ok(result);
    }
    
    
    [HttpGet]
    public IActionResult GetDetails()
    {
        var result = _driverService.GetAllDetails();
        return Ok(result);
    }
     
    [HttpGet]
    [Route("ForSelectBox")]
    public IActionResult GetForSelectBox()
    {
        var result = _driverService.GetForSelectBox();
        return Ok(result);
    }
    
    [HttpGet("{driverId}")]
    public IActionResult Get(int driverId)
    {
        var result = _driverService.GetById(driverId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(Driver driver)
    {
        var result = _driverService.Add(driver);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}
