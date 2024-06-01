using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IActivarHabitacion
{
    Task<Habitacion> ActivarRoom(int? id);
}
