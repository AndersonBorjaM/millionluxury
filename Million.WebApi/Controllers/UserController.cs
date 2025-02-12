using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Users.LoginUser;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            this._sender = sender;
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="user">Información del usuario.</param>
        /// <returns>Informacion del usuario registrado.</returns>
        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody]RegisterUserDTO user) => Ok( await _userService.RegisterUser(user));

        /// <summary>
        /// Método para generar un token.
        /// </summary>
        /// <param name="loginUser">Información del usuario.</param>
        /// <returns>Token</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUser, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new LoginCommand(loginUser.UserName, loginUser.Password), cancellationToken);
            
            if (result.IsFailure)
                return Unauthorized(result.Error);

           return Ok(result.Value);
        }
    }
}
