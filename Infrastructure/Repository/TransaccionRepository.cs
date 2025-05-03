using Microsoft.EntityFrameworkCore;
using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.Infrastructure.Data;


namespace UltimaPrueba.Infrastructure.Repository
{
    // Repositorio para acceder y manipular datos de transacciones en la base de datos
    // Implementa la interfaz ITransaccionRepository
    public class TransaccionRepository : ITransaccionRepository
    {
        // Contexto de base de datos de Entity Framework
        private readonly AppDbContext _context;

        // Constructor que recibe el contexto de base de datos mediante inyección de dependencias
        public TransaccionRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todas las transacciones de la base de datos
        // Incluye información relacionada de las cuentas origen y destino
        public async Task<List<Transaccion>> ObtenerTodasAsync()
        {
            return await _context.Transacciones
                .Include(t => t.CuentaOrigen)    // Carga la entidad CuentaOrigen relacionada
                .Include(t => t.CuentaDestino)    // Carga la entidad CuentaDestino relacionada
                .ToListAsync();
        }

        // Método para crear una nueva transacción en la base de datos
        // Recibe y guarda una entidad Transaccion y devuelve la misma con su ID generado
        public async Task<Transaccion> CrearAsync(Transaccion transaccion)
        {
            // Agrega la transacción al contexto
            _context.Transacciones.Add(transaccion);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelve la transacción con su ID generado
            return transaccion;
        }
    }
}
