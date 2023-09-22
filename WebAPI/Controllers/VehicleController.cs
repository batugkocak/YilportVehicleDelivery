using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
    [HttpGet]
    public IActionResult Get()
    {
        var result = _vehicleService.GetAll();
        return Ok(result.Data);
    }
    [HttpGet("{vehicleId}")]
    public IActionResult Get(int vehicleId)
    {
        var result = _vehicleService.GetById(vehicleId);
        return Ok(result.Data);
    }

    [HttpPost]
    public IActionResult Add(Vehicle vehicle)
    {
        var result = _vehicleService.Add(vehicle);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}