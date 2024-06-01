using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class PostNewRoom : IInsertarHabitacion
{
    private readonly DbHotelContext _context;

    public PostNewRoom(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<Habitacion> InsertarHabitacion(int? numeroHabitacion, int? piso, int? costoBase, int? impuesto, string? tipo, int? cantPersonas, string? ubicacion, string? nombreHotel)
    {
        var Hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Nombre == nombreHotel);
        if (Hotel == null) 
            throw new ArgumentNullException(nameof(Hotel));
        Habitacion newRoom = new Habitacion
        {
            Numero = numeroHabitacion,
            Piso = piso,
            CostoBase = costoBase,
            Impuestos = impuesto,
            TipoHabitacion = tipo,
            CantPersonas = cantPersonas,
            Ubicacion = ubicacion,
            Activa = 1,
            HotelId = Hotel.Id
        };
        await _context.Habitacions.AddAsync(newRoom);
        await _context.SaveChangesAsync();
        return newRoom;
    }
}
