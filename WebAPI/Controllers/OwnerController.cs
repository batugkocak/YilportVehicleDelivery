using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]s")]
[ApiController]
public class OwnerController : Controller
{ 
    private IOwnerService _ownerService;

    public OwnerController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }
    
    
    [HttpGet]
    public IActionResult Get()
    {
        var result = _ownerService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{ownerId}")]
    public IActionResult Get(int ownerId)
    {
        var result = _ownerService.GetById(ownerId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Add(Owner owner)
    {
        var result = _ownerService.Add(owner);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
}