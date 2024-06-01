namespace HOTEL_API.Aplicacion.Interfaces;

public interface IEstadoHabitacion
{
    Task<string> EstadoHabitacion(int? id);
}
