using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IReserva
{
    Task<int> AddReservaAsync(Reserva reserva, List<Huesped> huespedes, List<int> habitacionIds);
}
