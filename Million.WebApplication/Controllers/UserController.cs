﻿using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="user">Información del usuario.</param>
        /// <returns>Informacion del usuario registrado.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserDTO user) => Ok( await _userService.RegisterUser(user));

        /// <summary>
        /// Método para generar un token.
        /// </summary>
        /// <param name="loginUser">Información del usuario.</param>
        /// <returns>Token</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser) => Ok(await _userService.LoginUser(loginUser));
    }
}
