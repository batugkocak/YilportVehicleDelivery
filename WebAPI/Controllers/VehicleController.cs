using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]s")]
[ApiController]
public class VehicleController : Controller
{
    private IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }
    
    // [HttpGet]
    // public IActionResult Get()
    // {
    //     var result = _vehicleService.GetAll();
    //     return Ok(result);
    // }
    
    [Route("DetailsForTable")]
    [HttpGet]
    public IActionResult GetDetailed()
    {
        var result = _vehicleService.GetAllDetailsForTable();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("ForSelectBox")]
    public IActionResult GetForSelectBox()
    {
        var result = _vehicleService.GetForSelectBox();
        return Ok(result);
    }
    
    [Route("{vehicleId}/Details")]
    [HttpGet]
    public IActionResult GetDetailed(int vehicleId)
    {
        var result = _vehicleService.GetDetailsById(vehicleId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
    
    [HttpGet("{vehicleId}")]
    public IActionResult Get(int vehicleId)
    {
        var result = _vehicleService.GetById(vehicleId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
    

    [HttpPost]
    public IActionResult Post(Vehicle vehicle)
    {
        var result = _vehicleService.Add(vehicle);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [Route("CheckPlate")]
    [HttpGet]
    public IActionResult CheckByPlate(string plate)
    {
        var result = _vehicleService.CheckIfCarExistByPlate(plate);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok();
    }
    
    [Route("Update")]
    [HttpPost]
    public IActionResult Update(Vehicle vehicle)
    {
        var result = _vehicleService.Update(vehicle);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    [Route("Delete/{vehicleId}")]
    [HttpPost]
    public IActionResult Delete(int vehicleId)
    {
        var result = _vehicleService.Delete(vehicleId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}