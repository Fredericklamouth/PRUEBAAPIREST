using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Controllers
{
    // Controlador para gestionar las transacciones financieras
    // Proporciona endpoints para consultar y realizar transacciones
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        // Servicio que implementa la lógica de negocio para transacciones
        private readonly ITransaccionService _transaccionService;

        // Constructor que recibe el servicio de transacciones mediante inyección de dependencias
        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // Endpoint para obtener todas las transacciones
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTransacciones()
        {
            // Obtiene la lista de transacciones desde el servicio
            var transacciones = await _transaccionService.ObtenerTransaccionesAsync();
            return Ok(transacciones); // Devuelve la lista de transacciones con estado 200 OK
        }

        // Endpoint para realizar una nueva transacción
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RealizarTransaccion([FromBody] TrasaccionDto dto)
        {
            // Procesa la transacción a través del servicio
            var transaccion = await _transaccionService.RealizarTransaccionAsync(dto);
            return Ok(transaccion);
        }
    }
}
