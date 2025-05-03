using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        // Constructor que inyecta la configuración de la aplicación, necesaria para acceder a los valores del JWT.
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        /*
         Método que simula la autenticación de un usuario.
         Si el usuario y contraseña son válidos, genera un JWT con los parámetros definidos en la configuración.
         Retorna el token como string o null si las credenciales no coinciden.
         */
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Simulación: autenticar con usuario fijo
            if (loginDto.Username == "admin" && loginDto.Password == "1234")
            {
                // Se crean los claims para el token (en este caso, solo el nombre de usuario)
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, loginDto.Username)
            };

                // Se crea la clave de seguridad usando la clave secreta definida en el archivo de configuración
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Se construye el token JWT con issuer, audience, claims y expiración
                var token = new JwtSecurityToken(
                    issuer: _config["JwtSettings:Issuer"],
                    audience: _config["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:ExpirationMinutes"])),
                    signingCredentials: creds
                );

                // Se retorna el token generado como string
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            // Si las credenciales no coinciden, retorna null
            return null;
        }
    }
}
