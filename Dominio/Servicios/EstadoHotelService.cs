using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Dominio.Servicios;

public class EstadoHotelService : IEstadoHotel
{
    private readonly DbHotelContext _context;

    public EstadoHotelService(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<string> EstadoHotel(int? id)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        if (hotel == null)
            throw new ArgumentException(nameof(hotel));
        var mensaje = hotel.Activo == 1 ? "Activo" : "Inactivo";
        return mensaje;
    }
}
