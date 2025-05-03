using Microsoft.AspNetCore.Mvc;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Controllers
{
    // Controlador para manejar operaciones de autenticación en la API
    // Expone endpoints relacionados con la autenticación de usuarios
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Servicio de autenticación inyectado mediante inyección de dependencias
        private readonly IAuthService _authService;

        // Constructor del controlador de autenticación
        // Recibe el servicio de autenticación que será inyectado
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint para iniciar sesión de usuarios
        // Recibe credenciales y devuelve un token de autenticación si son válidas
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Intenta autenticar al usuario a través del servicio
            var token = await _authService.LoginAsync(dto);

            // Si no se generó un token, las credenciales son incorrectas
            if (token == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            // Devuelve el token en formato JSON
            return Ok(new { token });
        }
    }
}
