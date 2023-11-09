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
    [Route("ForSelectBox")]
    public IActionResult GetForSelectBox()
    {
        var result = _driverService.GetForSelectBox();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("ForTable")]
    public IActionResult GetForTable()
    {
        var result = _driverService.GetForTable();
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
    [Route("Delete/{driverId}")]
    [HttpPost]
    public IActionResult Delete(int driverId)
    {
        var result = _driverService.Delete(driverId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Update")]
    [HttpPost]
    public IActionResult Update(Driver driver)
    {
        var result = _driverService.Update(driver);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
