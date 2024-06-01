using HOTEL_API.Aplicacion.Interfaces;

namespace HOTEL_API.Infrastructura.Repositorios
{
    public class AddReserva : IReserva
    {
        private readonly DbHotelContext _context;

        public AddReserva(DbHotelContext context)
        {
            _context = context;
        }
        public async Task<int> AddReservaAsync(Reserva reserva,List<Huesped> huespedes, List<int> habitacionIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Reservas.AddAsync(reserva);
                await _context.SaveChangesAsync();

                foreach (var huesped in huespedes)
                {
                    await _context.Huespeds.AddAsync(huesped);
                    await _context.SaveChangesAsync();

                    var huespedReserva = new HuespedReserva
                    {
                        HuespedId = huesped.Id,
                        ReservaId = reserva.Id
                    };

                    await _context.HuespedReservas.AddAsync(huespedReserva);
                    await _context.SaveChangesAsync();
                }

                foreach (var habitacionId in habitacionIds)
                {
                    var habitacionReserva = new HabitacionReserva
                    {
                        HabitacionId = habitacionId,
                        ReservaId = reserva.Id
                    };

                    await _context.HabitacionReservas.AddAsync(habitacionReserva);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return reserva.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ArgumentNullException(nameof(ex));
            }           

        }
    }
}
