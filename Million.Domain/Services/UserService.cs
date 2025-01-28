using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Million.Domain.DTO;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Domain.Validations;

namespace Million.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="user">Información del usuario</param>
        /// <returns>Informacion del usuario registrado.</returns>
        /// <exception cref="ArgumentException">Mensajes de error en el caso de información invalida.</exception>
        public async Task<User> RegisterUser(RegisterUserDTO user)
        {
            RegisterUserValidation validationRules = new RegisterUserValidation();
            var resultValidation = validationRules.Validate(user);

            if (!resultValidation.IsValid)
                throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));

            if ((await _userRepository.GetUserByUserName(user.UserName)) != null)
                throw new ArgumentException("The user is already registered");

            return await _userRepository.CreateAsync(_mapper.Map<User>(user));
        }


        /// <summary>
        /// Método para autenticar un usuario.
        /// </summary>
        /// <param name="userDto">Información del usuario que se esta autenticando.</param>
        /// <returns>Token</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<string> LoginUser(LoginUserDTO userDto)
        {
            var user = await _userRepository.GetUserByUserName(userDto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password.");

            return GenerateJwtToken(userDto);
        }

        /// <summary>
        /// Método para generar el token de un usuario.
        /// </summary>
        /// <param name="user">Información del usuario</param>
        /// <returns>Token</returns>
        private string GenerateJwtToken(LoginUserDTO user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"].ToString()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
