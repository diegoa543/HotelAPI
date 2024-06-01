using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Dominio.Servicios;

public class EstadoHabitacionServices : IEstadoHabitacion
{
    private readonly DbHotelContext _context;

    public EstadoHabitacionServices(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<string> EstadoHabitacion(int? id)
    {
        var habitacion = await _context.Habitacions.FirstOrDefaultAsync(x => x.Id == id);
        if (habitacion == null)
            throw new ArgumentException(nameof(habitacion));
        var mensaje = habitacion.Activa == 1 ? "Activo" : "Inactivo";
        return mensaje;
    }
}
