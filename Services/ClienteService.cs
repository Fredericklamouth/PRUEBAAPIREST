using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;
using UltimaPrueba.Infrastructure.Repository;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Services
{
    /*
     * Servicio que implementa la lógica de negocio relacionada con los clientes.
     * Utiliza un repositorio para interactuar con la capa de datos.
     */
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        // Constructor que inyecta el repositorio de cliente
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // Obtiene todos los clientes del sistema
        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            return await _clienteRepository.ObtenerTodosAsync();
        }

        // Crea un nuevo cliente
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            return await _clienteRepository.CrearClienteAsync(cliente);
        }

        /*
         * Actualiza los datos de un cliente existente.
         * Devuelve false si el cliente no existe.
         */
        public async Task<bool> ActualizarClienteAsync(ClienteUpdateDto clienteDto)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(clienteDto.Id);
            if (cliente == null)
                return false;

            // Actualiza los campos con los valores del DTO
            cliente.Nombre = clienteDto.Nombre;
            cliente.Apellido = clienteDto.Apellido;
            cliente.Cedula = clienteDto.Cedula;
            cliente.Email = clienteDto.Email;
            cliente.Direccion = clienteDto.Direccion;
            cliente.Telefono = clienteDto.Telefono;

            await _clienteRepository.ActualizarClienteAsync(cliente);
            return true;
        }

        /*
         * Elimina un cliente por su ID.
         * Devuelve false si el cliente no existe.
         */
        public async Task<bool> EliminarClienteAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (cliente == null)
                return false;

            await _clienteRepository.EliminarClienteAsync(cliente);
            return true;
        }

        /*
         * Obtiene el balance total del cliente sumando los saldos de todas sus cuentas.
         * Lanza una excepción si el cliente no existe.
         */
        public async Task<decimal> ObtenerBalanceClienteAsync(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObtenerPorIdConCuentasAsync(clienteId);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            // Suma los saldos de las cuentas o retorna 0 si no tiene cuentas
            return cliente.Cuentas?.Sum(c => c.Saldo) ?? 0;
        }
    }
}

