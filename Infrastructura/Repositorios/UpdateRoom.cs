using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class UpdateRoom : IActualizarHabitacion
{
    private readonly DbHotelContext _context;

    public UpdateRoom(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<Habitacion> UpdateHabitacionExistente(int? id, int? numeroHabitacion, int? piso, int? costoBase, int? impuesto, string? tipo, int? cantPersonas, string? ubicacion, string? nombreHotel)
    {
        var habitacion = await _context.Habitacions.FirstOrDefaultAsync(h => h.Id == id);
        if (habitacion == null)
            throw new ArgumentNullException(nameof(habitacion));

        var Hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Nombre == nombreHotel);
        if (Hotel == null)
            throw new ArgumentNullException(nameof(Hotel));

        habitacion.Numero = numeroHabitacion;
        habitacion.Piso = piso;
        habitacion.CostoBase = costoBase;
        habitacion.Impuestos = impuesto;
        habitacion.TipoHabitacion = tipo;
        habitacion.CantPersonas = cantPersonas;
        habitacion.Ubicacion = ubicacion;
        habitacion.HotelId = Hotel.Id;
        await _context.SaveChangesAsync();
        return habitacion;

    }
}
