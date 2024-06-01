using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces
{
    public interface IInsertarHabitacion
    {
        Task<Habitacion> InsertarHabitacion(int? numeroHabitacion,int? piso, int? costoBase, int? impuesto, string? tipo, int? cantPersonas, string? ubicacion, string? nombreHotel);
    }
}
