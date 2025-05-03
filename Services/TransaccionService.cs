using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;
using UltimaPrueba.Services.Interface;

namespace UltimaPrueba.Services
{
    public class TransaccionService : ITransaccionService
    {
        // Repositorio para acceder a las cuentas
        private readonly ICuentaRepository _cuentaRepository;

        // Repositorio para registrar y obtener transacciones
        private readonly ITransaccionRepository _transaccionRepository;

        // Constructor con inyección de dependencias de los repositorios necesarios
        public TransaccionService(ICuentaRepository cuentaRepository, ITransaccionRepository transaccionRepository)
        {
            _cuentaRepository = cuentaRepository;
            _transaccionRepository = transaccionRepository;
        }

        /*
         Obtiene todas las transacciones registradas en el sistema
         */
        public async Task<List<Transaccion>> ObtenerTransaccionesAsync()
        {
            return await _transaccionRepository.ObtenerTodasAsync();
        }

        /*
         Realiza una transacción de transferencia entre dos cuentas.
         Verifica que ambas cuentas existan y que la cuenta origen tenga suficiente saldo.
         Actualiza los saldos de ambas cuentas y registra la transacción.
         */
        public async Task<Transaccion> RealizarTransaccionAsync(TrasaccionDto dto)
        {
            // Buscar la cuenta de origen y destino
            var origen = await _cuentaRepository.ObtenerPorIdAsync(dto.CuentaOrigenId);
            var destino = await _cuentaRepository.ObtenerPorIdAsync(dto.CuentaDestinoId);

            // Validar que ambas cuentas existan
            if (origen == null || destino == null)
                throw new ArgumentException("Cuenta origen o destino no encontrada");

            // Validar que la cuenta de origen tenga suficiente saldo
            if (origen.Saldo < dto.Monto)
                throw new InvalidOperationException("Saldo insuficiente en cuenta origen");

            // Realizar la transferencia de saldo
            origen.Saldo -= dto.Monto;
            destino.Saldo += dto.Monto;

            // Crear el objeto de transacción con los datos recibidos
            var transaccion = new Transaccion
            {
                CuentaOrigenId = dto.CuentaOrigenId,
                CuentaDestinoId = dto.CuentaDestinoId,
                Monto = dto.Monto,
                Tipo = dto.Tipo,
                Descripcion = dto.Descripcion,
                Fecha = DateTime.UtcNow
            };

            // Guardar los cambios de saldo en ambas cuentas
            await _cuentaRepository.ActualizarAsync(origen);
            await _cuentaRepository.ActualizarAsync(destino);

            // Registrar la transacción en la base de datos
            return await _transaccionRepository.CrearAsync(transaccion);
        }
    }
}
