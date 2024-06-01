using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class ActivarHabitacionPost :IActivarHabitacion
{
    private readonly DbHotelContext _context;
    private readonly IEstadoHabitacion _estado;

    public ActivarHabitacionPost(DbHotelContext context, IEstadoHabitacion estado)
    {
        _context = context;
        _estado = estado;
    }
    public async Task<Habitacion> ActivarRoom(int? id)
    {
        var estado = _estado.EstadoHabitacion(id).Result;
        if (estado == "Activo")
            throw new Exception("La habitación ya se encuentra activo");
        var room = await _context.Habitacions.FirstOrDefaultAsync(x => x.Id == id);
        if (room == null) throw new ArgumentException(nameof(room));
        room.Activa = 1;
        await _context.SaveChangesAsync();
        return room;
    }
}
