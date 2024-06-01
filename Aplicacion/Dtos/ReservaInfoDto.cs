namespace HOTEL_API.Aplicacion.Dtos
{
    public class ReservaInfoDto
    {
        public string HotelNombre { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int HabitacionNumero { get; set; }
        public string? TipoHabitacion { get; set; }

    }
}
