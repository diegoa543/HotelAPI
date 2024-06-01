using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IInsertarHotel
{
    Task<Hotel> InsertarHotel(string? nombre, string? direccion, string? ciudad);
}
