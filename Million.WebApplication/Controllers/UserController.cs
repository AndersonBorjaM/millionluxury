using Microsoft.AspNetCore.Mvc;
using Million.Domain.DTO;
using Million.Domain.Services;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserDTO user) => Ok( await _userService.RegisterUser(user));

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser) => Ok(await _userService.LoginUser(loginUser));
    }
}
