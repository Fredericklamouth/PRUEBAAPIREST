using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;
using UltimaPrueba.Services;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Controllers
{

    // Controlador para gestionar operaciones relacionadas con los clientes
    // Proporciona endpoints para crear, leer, actualizar y eliminar clientes
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        // Servicio que implementa la lógica de negocio para clientes
        private readonly IClienteService _clienteService;

        // Constructor que recibe el servicio de clientes mediante inyección de dependencias
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Endpoint para obtener la lista de todos los clientes
        // No requiere autenticación
        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> GetClientes()
        {
            // Obtiene los clientes de la base de datos
            var clientes = await _clienteService.ObtenerClientesAsync();

            // Mapea los modelos de dominio a DTOs para la respuesta
            var clientesDto = clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Cedula = c.Cedula,
                Email = c.Email,
                Direccion = c.Direccion,
                Telefono = c.Telefono
            }).ToList();

            // Devuelve la lista de clientes con estado 200 OK
            return Ok(clientesDto);
        }

        // Endpoint para crear un nuevo cliente
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] ClienteCreateDto clienteDto)
        {
            // Crea una nueva entidad Cliente a partir de los datos del DTO
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Cedula = clienteDto.Cedula,
                Email = clienteDto.Email,
                Direccion = clienteDto.Direccion,
                Telefono = clienteDto.Telefono
            };

            // Guarda el cliente en la base de datos
            var nuevoCliente = await _clienteService.CrearClienteAsync(cliente);

            // Devuelve respuesta 201 Created con la ubicación del recurso creado
            return CreatedAtAction(nameof(GetClientes), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        // Endpoint para actualizar los datos de un cliente existente
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(Guid id, [FromBody] ClienteUpdateDto clienteDto)
        {
            // Verifica que el ID en la URL coincida con el del cuerpo
            if (id != clienteDto.Id)
                return BadRequest("El ID en la URL no coincide con el del cuerpo de la solicitud.");

            // Actualiza el cliente en la base de datos
            var resultado = await _clienteService.ActualizarClienteAsync(clienteDto);

            // Si no se encontró el cliente, devuelve 404 Not Found
            if (!resultado)
                return NotFound();

            // Si la actualización fue exitosa, devuelve 204 No Content
            return NoContent();
        }

        // Endpoint para eliminar un cliente por su ID
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(Guid id)
        {
            // Intenta eliminar el cliente de la base de datos
            var eliminado = await _clienteService.EliminarClienteAsync(id);

            // Si no se encontró el cliente, devuelve 404 Not Found
            if (!eliminado)
                return NotFound();

            // Si la eliminación fue exitosa, devuelve 204 No Content
            return NoContent();
        }

        // Endpoint para obtener el balance financiero de un cliente
        // Requiere que el usuario esté autenticado
        [Authorize]
        [HttpGet("{id}/balance")]
        public async Task<ActionResult<decimal>> ObtenerBalance(Guid id)
        {
            try
            {
                // Obtiene el balance del cliente desde el servicio
                var balance = await _clienteService.ObtenerBalanceClienteAsync(id);

                // Devuelve el balance con estado 200 OK
                return Ok(balance);
            }
            catch (Exception e)
            {
                // Si ocurre un error (cliente no encontrado), devuelve 404 con el mensaje de error
                return NotFound(e.Message);
            }
        }
    }
}
