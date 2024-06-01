using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HOTEL_API.Infrastructura.Repositorios;

public class BuscarAlojamientoGet : IBuscarAlojamiento
{
    private readonly DbHotelContext _context;

    public BuscarAlojamientoGet(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<List<Habitacion>> BuscarAlojamientoPorFecha(string? cuidad, DateTime? fechaInicio, DateTime? fechaFin, int? cantPersonas)
    {
        var ciudadEncontrada = await _context.Ciudads.FirstOrDefaultAsync(c => c.Nombre == cuidad);
        if (ciudadEncontrada == null)
        {
            return new List<Habitacion>();
            throw new Exception("No se encuentra la ciudad");
        }

        var habitacionesDisponibles = await _context.Habitacions
            .Include(h => h.Hotel)
            .Where(h => h.CantPersonas >= cantPersonas && h.Hotel.CiudadId == ciudadEncontrada.Id && h.Activa == 1)
            .Where(h => !_context.HabitacionReservas.Any(hr => hr.HabitacionId == h.Id &&
                     ((hr.Reserva.FechaInicio <= fechaInicio && hr.Reserva.FechaFin >= fechaInicio) ||
                      (hr.Reserva.FechaInicio <= fechaFin && hr.Reserva.FechaFin >= fechaFin))))
            .ToListAsync();


        return habitacionesDisponibles;
    }
}
