using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class InactivarHotelPost : IInactivarHotel
{
    private DbHotelContext _context;
    private readonly IEstadoHotel _estadoHotel;

    public InactivarHotelPost(DbHotelContext context, IEstadoHotel estadoHotel)
    {
        _context = context;
        _estadoHotel = estadoHotel;
    }
    public async Task<Hotel> InactivateHotel(int? id)
    {
        var estado = _estadoHotel.EstadoHotel(id).Result;
        if (estado == "Inactivo")
            throw new Exception("El hotel ya se encuentra Inactivo");
        var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        if (hotel == null) throw new ArgumentException(nameof(hotel));
        hotel.Activo = 0;
        await _context.SaveChangesAsync();
        return hotel;
    }
}
