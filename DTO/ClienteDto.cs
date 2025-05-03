namespace UltimaPrueba.DTO
{

    //DTO Que me ayuda a presentar solos los datos que me interesan del cliente a la hora de crearlos
    public class ClienteCreateDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }

    //Esto me ayuda a controlar la informacion a la hora de actualizar los clientes
    public class ClienteUpdateDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }

    //manejo la informacion que sale a la hora de Obtener la informacion de un cliente
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}

