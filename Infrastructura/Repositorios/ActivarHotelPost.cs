using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios
{
    public class ActivarHotelPost : IActivarHotel
    {
        private readonly DbHotelContext _context;
        private readonly IEstadoHotel _estadoHotel;

        public ActivarHotelPost(DbHotelContext context, IEstadoHotel estadoHotel)
        {
            _context = context;
            _estadoHotel = estadoHotel;
        }
        public async Task<Hotel> ActiveHotel(int? id)
        {
            var estado = _estadoHotel.EstadoHotel(id).Result;
            if (estado == "Activo")
                throw new Exception("El hotel ya se encuentra activo");
            var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
            if (hotel == null) throw new ArgumentException(nameof(hotel));
            hotel.Activo = 1;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
