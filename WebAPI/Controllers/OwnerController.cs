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
    [HttpGet]
    [Route("ForSelectBox")]
    public IActionResult GetForSelectBox()
    {
        var result = _ownerService.GetForSelectBox();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("ForTable")]
    public IActionResult GetForTable()
    {
        var result = _ownerService.GetForTable();
        return Ok(result);
    }
    [HttpGet("{ownerId}")]
    public IActionResult Get(int ownerId)
    {
        var result = _ownerService.GetById(ownerId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(Owner owner)
    {
        var result = _ownerService.Add(owner);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    [Route("Delete/{ownerId}")]
    [HttpPost]
    public IActionResult Delete(int ownerId)
    {
        var result = _ownerService.Delete(ownerId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
    
    [Route("Update")]
    [HttpPost]
    public IActionResult Update(Owner owner)
    {
        var result = _ownerService.Update(owner);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}