using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IActualizarHabitacion
{
    Task<Habitacion> UpdateHabitacionExistente(int? id, int? numeroHabitacion, int? piso, int? costoBase, int? impuesto, string? tipo, int? cantPersonas, string? ubicacion, string? nombreHotel);
}
