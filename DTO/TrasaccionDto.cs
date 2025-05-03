namespace UltimaPrueba.DTO
{
    //Dto para manejar los datos que se obtienene y se llenan a la hora de Crear una transaccion
    public class TrasaccionDto
    {
        public int CuentaOrigenId { get; set; }
        public int CuentaDestinoId { get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
