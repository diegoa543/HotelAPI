using HOTEL_API.Aplicacion.Dtos;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IListarReservas
{
    Task<IEnumerable<ListaReservasDto>> ListarReservasAsync();
}
