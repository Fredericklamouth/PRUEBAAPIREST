using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Services
{
    /*
         Servicio que gestiona la lógica relacionada con las cuentas bancarias.
         Interactúa con el repositorio para realizar operaciones CRUD y de saldo.
         */
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepo;

        // Constructor que inyecta el repositorio de cuentas
        public CuentaService(ICuentaRepository cuentaRepo)
        {
            _cuentaRepo = cuentaRepo;
        }

        // Obtiene todas las cuentas del sistema y las transforma a DTOs
        public async Task<List<CuentaDto>> ObtenerCuentasAsync()
        {
            var cuentas = await _cuentaRepo.ObtenerTodasAsync();
            return cuentas.Select(c => new CuentaDto
            {
                Id = c.Id,
                NumeroCuenta = c.NumeroCuenta,
                Saldo = c.Saldo,
                ClienteId = c.ClienteId
            }).ToList();
        }

        // Crea una nueva cuenta a partir de los datos del DTO
        public async Task<CuentaDto> CrearCuentaAsync(CuentaCreateDto dto)
        {
            var cuenta = new Cuenta
            {
                NumeroCuenta = dto.NumeroCuenta,
                Saldo = dto.Saldo,
                ClienteId = dto.ClienteId
            };

            var creada = await _cuentaRepo.CrearAsync(cuenta);

            // Devuelve los datos de la cuenta creada como DTO
            return new CuentaDto
            {
                Id = creada.Id,
                NumeroCuenta = creada.NumeroCuenta,
                Saldo = creada.Saldo,
                ClienteId = creada.ClienteId
            };
        }

        /*
         * Actualiza los datos de una cuenta existente.
         * Devuelve false si la cuenta no existe.
         */
        public async Task<bool> ActualizarCuentaAsync(CuentaDto dto)
        {
            var cuenta = await _cuentaRepo.ObtenerPorIdAsync(dto.Id);
            if (cuenta == null) return false;

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.Saldo = dto.Saldo;

            return await _cuentaRepo.ActualizarAsync(cuenta);
        }

        // Elimina una cuenta por su ID
        public async Task<bool> EliminarCuentaAsync(int id)
        {
            return await _cuentaRepo.EliminarAsync(id);
        }

        /*
         * Agrega saldo a una cuenta específica.
         * Devuelve false si la cuenta no existe.
         */
        public async Task<bool> AgregarSaldoAsync(AgregarSaldoDto dto)
        {
            var cuenta = await _cuentaRepo.ObtenerPorIdAsync(dto.CuentaId);
            if (cuenta == null) return false;

            cuenta.Saldo += dto.Monto;
            return await _cuentaRepo.ActualizarAsync(cuenta);
        }
    }
}


