using Microsoft.EntityFrameworkCore;
using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.Infrastructure.Data;

namespace UltimaPrueba.Infrastructure.Repository
{
    // Repositorio para acceder y manipular datos de cuentas en la base de datos
    // Implementa la interfaz ICuentaRepository con operaciones CRUD
    public class CuentaRepository : ICuentaRepository
    {
        // Contexto de base de datos de Entity Framework
        private readonly AppDbContext _context;

        // Constructor que recibe el contexto de base de datos mediante inyección de dependencias
        public CuentaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para crear una nueva cuenta en la base de datos
        // Recibe y guarda una entidad Cuenta y devuelve la misma con su ID generado
        public async Task<Cuenta> CrearAsync(Cuenta cuenta)
        {
            // Agrega la cuenta al contexto
            _context.Cuentas.Add(cuenta);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelve la cuenta con su ID generado
            return cuenta;
        }

        // Método para eliminar una cuenta por su ID
        // Devuelve true si la cuenta fue eliminada, false si no se encontró
        public async Task<bool> EliminarAsync(int id)
        {
            // Busca la cuenta por su ID
            var cuenta = await _context.Cuentas.FindAsync(id);

            // Si no se encontró la cuenta, devuelve false
            if (cuenta == null) return false;

            // Elimina la cuenta del contexto
            _context.Cuentas.Remove(cuenta);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Indica que la eliminación fue exitosa
            return true;
        }

        // Método para obtener todas las cuentas de la base de datos
        public async Task<List<Cuenta>> ObtenerTodasAsync()
        {
            // Retorna todas las cuentas como una lista
            return await _context.Cuentas.ToListAsync();
        }

        // Método para obtener una cuenta específica por su ID
        // Devuelve null si la cuenta no existe
        public async Task<Cuenta> ObtenerPorIdAsync(int id)
        {
            // Busca y devuelve la cuenta con el ID especificado
            return await _context.Cuentas.FindAsync(id);
        }

        // Método para actualizar una cuenta existente
        // Devuelve true si la actualización fue exitosa
        public async Task<bool> ActualizarAsync(Cuenta cuenta)
        {
            // Marca la entidad cuenta como modificada en el contexto
            _context.Cuentas.Update(cuenta);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Indica que la actualización fue exitosa
            return true;
        }
    }
}