using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class UpdateHotel : IActualizarHotel
{
    private readonly DbHotelContext _context;

    public UpdateHotel(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<Hotel> UpdateHotelExistente(int? id, string? nombre, string? direccion, string? ciudad)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        if (hotel == null) 
            throw new ArgumentNullException(nameof(hotel));

        var ciudadNombre = await _context.Ciudads.FirstOrDefaultAsync(x => x.Nombre == ciudad);
        if (ciudadNombre == null)
            throw new ArgumentNullException(nameof(ciudadNombre));

        hotel.Nombre = nombre;
        hotel.Direccion = direccion;
        hotel.CiudadId = ciudadNombre.Id;
        await _context.SaveChangesAsync();
        return hotel;
    }
}
