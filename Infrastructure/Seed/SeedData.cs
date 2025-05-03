using UltimaPrueba.Domain.Model;
using UltimaPrueba.Infrastructure.Data;

namespace UltimaPrueba.Infrastructure.Seed
{

    // La clase SeedData y la funcion Inicializar me permite agregar datos a la hora de iniciar el programa y tener datos registrados para hacer pruebas 
    public static class SeedData
    {

        //Funcion Inicializar
        public static void Inicializar(AppDbContext context)
        {
            if (!context.Clientes.Any())
            {
                //Creacion de un Cliente
                var cliente1 = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Carlos",
                    Apellido = "Ramírez",
                    Cedula = "1111111111",
                    Email = "carlos@correo.com",
                    Direccion = "Av. Central 123",
                    Telefono = "0999999999",
                    Cuentas = new List<Cuenta>()
                };

                //Creacion de una Tienda
                var cuenta1 = new Cuenta
                {
                    NumeroCuenta = "ACC-0001",
                    Saldo = 1500m,
                    ClienteId = cliente1.Id
                };

                var cliente2 = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Ana",
                    Apellido = "López",
                    Cedula = "2222222222",
                    Email = "ana@correo.com",
                    Direccion = "Calle 10",
                    Telefono = "0888888888",
                    Cuentas = new List<Cuenta>()
                };

                var cuenta2 = new Cuenta
                {
                    NumeroCuenta = "ACC-0002",
                    Saldo = 2300m,
                    ClienteId = cliente2.Id
                };

                cliente1.Cuentas.Add(cuenta1);
                cliente2.Cuentas.Add(cuenta2);

                context.Clientes.AddRange(cliente1, cliente2);
                context.Cuentas.AddRange(cuenta1, cuenta2);
                context.SaveChanges();
            }

            //Creacion de un Usuario
            if (!context.Usuarios.Any())
            {
                var user = new Usuario
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "123456",
                    Email = "Ejemplo@gmail.com"
                };

                context.Usuarios.Add(user);
                context.SaveChanges();
            }
        }
    }
}

