using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class InactivarHabitacionPost :IInactivarHabitacion
{
    private DbHotelContext _context;
    private readonly IEstadoHabitacion _estadoHabitacion;

    public InactivarHabitacionPost(DbHotelContext context, IEstadoHabitacion estadoHabitacion)
    {
        _context = context;
        _estadoHabitacion = estadoHabitacion;
    }
    public async Task<Habitacion> InactivarRoom(int? id)
    {
        var estado = _estadoHabitacion.EstadoHabitacion(id).Result;
        if (estado == "Inactivo")
            throw new Exception("La habitación ya se encuentra Inactiva");
        var room = await _context.Habitacions.FirstOrDefaultAsync(x => x.Id == id);
        if (room == null) throw new ArgumentException(nameof(room));
        room.Activa = 0;
        await _context.SaveChangesAsync();
        return room;
    }
}
