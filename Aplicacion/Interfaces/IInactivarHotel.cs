using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IInactivarHotel
{
    Task<Hotel> InactivateHotel(int? id);
}
