namespace HOTEL_API.Aplicacion.Interfaces;

public interface IEstadoHotel
{
    Task<string> EstadoHotel(int? id);
}
