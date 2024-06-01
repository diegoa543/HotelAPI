using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IActivarHotel
{
    Task<Hotel> ActiveHotel(int? id);
}
