using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Dtos;

public class ReservaDto
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int UsuarioId { get; set; }
    public string? NmContectoEmergencia { get; set; }
    public string? NumeroContacto { get; set; }
    public List<Huesped> Huespedes { get; set; }
    public List<int> HabitacionIds { get; set; }

}
