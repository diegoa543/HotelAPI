using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class GetReservas : IListarReservas
{
    private readonly DbHotelContext _context;

    public GetReservas(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ListaReservasDto>> ListarReservasAsync()
    {
        var reservas = await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.HabitacionReservas)
                    .ThenInclude(hr => hr.Habitacion)
                        .ThenInclude(h => h.Hotel)
                .Select(r => new ListaReservasDto
                {
                    NombreHotel = r.HabitacionReservas
                                    .Select(hr => hr.Habitacion.Hotel.Nombre)
                                    .FirstOrDefault(),
                    FechaInicio = r.FechaInicio.Value,
                    FechaFin = r.FechaFin.Value,
                    NumeroHabitacion = r.HabitacionReservas
                                        .Select(hr => hr.Habitacion.Numero)
                                        .FirstOrDefault(),
                    TipoHabitacion = r.HabitacionReservas
                                        .Select(hr => hr.Habitacion.TipoHabitacion)
                                        .FirstOrDefault(),
                    NombreUsuario = r.Usuario.Nombre                    
                }).ToListAsync();

        return reservas;
    }
}
