using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]s")]
[ApiController]
public class BrandController : Controller
{
    private IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var result = _brandService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{brandId}")]
    public IActionResult Get(int brandId)
    {
        var result = _brandService.GetById(brandId);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Add(Brand brand)
    {
        var result = _brandService.Add(brand);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}