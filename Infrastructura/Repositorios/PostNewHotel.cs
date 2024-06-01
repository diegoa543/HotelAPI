using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class PostNewHotel : IInsertarHotel
{
    private readonly DbHotelContext _context;

    public PostNewHotel(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<Hotel> InsertarHotel(string? nombre, string? direccion, string? ciudad)
    {
        var ciudadNombre = await _context.Ciudads.FirstOrDefaultAsync(x => x.Nombre == ciudad);
        if (ciudadNombre == null)
            throw new ArgumentNullException(nameof(ciudadNombre));
        Hotel newHotel = new Hotel
        {
            Nombre = nombre,
            Direccion = direccion,
            CiudadId = ciudadNombre.Id,
            Activo = 1
        };
        await _context.Hotels.AddAsync(newHotel);
        await _context.SaveChangesAsync();
        return newHotel;
    }
}
