namespace HOTEL_API.Aplicacion.Dtos
{
    public class ListaReservasDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreHotel { get; set; }
        public int? NumeroHabitacion { get; set; }
        public string? TipoHabitacion { get; set; }

    }
}
