using Business.Abstract;
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
    // GET
    [HttpGet("{brandId}")]
    public IActionResult Get(int brandId)
    {
        var result = _brandService.GetById(brandId);
        return Ok(result.Data);
    }
}