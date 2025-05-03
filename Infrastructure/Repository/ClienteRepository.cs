using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UltimaPrueba.Domain.Interfaces;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.Infrastructure.Data;

namespace UltimaPrueba.Infrastructure.Repository
{
    // Repositorio para acceder y manipular datos de clientes en la base de datos
    // Implementa la interfaz IClienteRepository con operaciones CRUD
    public class ClienteRepository : IClienteRepository
    {
        // Contexto de base de datos de Entity Framework
        private readonly AppDbContext _context;

        // Constructor que recibe el contexto de base de datos mediante inyección de dependencias
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los clientes incluyendo sus cuentas relacionadas
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            // Retorna todos los clientes como una lista e incluye la colección Cuentas
            return await _context.Clientes.Include(c => c.Cuentas).ToListAsync();
        }

        // Método para crear un nuevo cliente en la base de datos
        // Recibe y guarda una entidad Cliente y devuelve la misma con su ID generado
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            // Agrega el cliente al contexto
            _context.Clientes.Add(cliente);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelve el cliente con su ID generado
            return cliente;
        }

        // Método para obtener un cliente específico por su ID
        // Puede devolver null si el cliente no existe
        public async Task<Cliente?> ObtenerPorIdAsync(Guid id)
        {
            // Busca y devuelve el cliente con el ID especificado
            return await _context.Clientes.FindAsync(id);
        }

        // Método para actualizar un cliente existente
        // Devuelve el cliente actualizado
        public async Task<Cliente> ActualizarClienteAsync(Cliente cliente)
        {
            // Marca la entidad cliente como modificada en el contexto
            _context.Clientes.Update(cliente);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelve el cliente actualizado
            return cliente;
        }

        // Método para eliminar un cliente
        // Recibe una entidad Cliente ya cargada
        public async Task EliminarClienteAsync(Cliente cliente)
        {
            // Elimina el cliente del contexto
            _context.Clientes.Remove(cliente);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();
        }

        // Método para obtener un cliente por su ID incluyendo sus cuentas relacionadas
        // Utiliza FirstOrDefaultAsync para devolver null si no encuentra el cliente
        public async Task<Cliente> ObtenerPorIdConCuentasAsync(Guid id)
        {
            // Busca un cliente por ID y carga la colección Cuentas relacionadas
            return await _context.Clientes
                .Include(c => c.Cuentas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}