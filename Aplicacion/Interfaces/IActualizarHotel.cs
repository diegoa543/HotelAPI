using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IActualizarHotel
{
    Task<Hotel> UpdateHotelExistente(int? id, string? nombre, string? direccion, string? ciudad);
}
