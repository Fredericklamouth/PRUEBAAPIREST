using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Controllers
{
    // Controlador para la gestión de cuentas bancarias
    // Proporciona endpoints para consultar y manipular cuentas
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : ControllerBase
    {
        // Servicio que implementa la lógica de negocio para cuentas
        private readonly ICuentaService _cuentaService;

        // Constructor que recibe el servicio de cuentas mediante inyección de dependencias
        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        // Endpoint para obtener todas las cuentas
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CuentaDto>>> Get()
        {
            // Obtiene y devuelve la lista de cuentas con estado 200 OK
            return Ok(await _cuentaService.ObtenerCuentasAsync());
        }

        // Endpoint para crear una nueva cuenta
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CuentaDto>> Post([FromBody] CuentaCreateDto dto)
        {
            // Crea una nueva cuenta en la base de datos
            var cuenta = await _cuentaService.CrearCuentaAsync(dto);

            // Devuelve respuesta 201 Created con la ubicación del recurso creado
            return CreatedAtAction(nameof(Get), new { id = cuenta.Id }, cuenta);
        }

        // Endpoint para actualizar los datos de una cuenta existente
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CuentaDto dto)
        {
            // Actualiza la cuenta en la base de datos
            var result = await _cuentaService.ActualizarCuentaAsync(dto);

            // Si no se encontró la cuenta, devuelve 404 Not Found
            if (!result) return NotFound();

            // Si la actualización fue exitosa, devuelve 204 No Content
            return NoContent();
        }

        // Endpoint para eliminar una cuenta por su ID
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Intenta eliminar la cuenta de la base de datos
            var result = await _cuentaService.EliminarCuentaAsync(id);

            // Si no se encontró la cuenta, devuelve 404 Not Found
            if (!result) return NotFound();

            // Si la eliminación fue exitosa, devuelve 204 No Content
            return NoContent();
        }

        // Endpoint para agregar saldo a una cuenta existente
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPost("agregar-saldo")]
        public async Task<IActionResult> AgregarSaldo([FromBody] AgregarSaldoDto dto)
        {
            // Intenta agregar saldo a la cuenta
            var result = await _cuentaService.AgregarSaldoAsync(dto);

            // Si no se encontró la cuenta, devuelve 404 Not Found con mensaje
            if (!result) return NotFound("Cuenta no encontrada.");

            // Si la operación fue exitosa, devuelve 200 OK con mensaje de confirmación
            return Ok("Saldo agregado correctamente.");
        }
    }
}