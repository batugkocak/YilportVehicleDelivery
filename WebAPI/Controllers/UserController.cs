using Entities.DTOs.User;

using Business.Abstract;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController:Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("ForTable")]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _userService.GetForList();
            return Ok(result);
        }
    }
}