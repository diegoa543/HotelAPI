using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IInactivarHabitacion
{
    Task<Habitacion> InactivarRoom(int? id);
}
