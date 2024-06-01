using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class HabitacionReserva
{
    public int Id { get; set; }

    public int HabitacionId { get; set; }

    public int ReservaId { get; set; }

    public virtual Habitacion Habitacion { get; set; } = null!;

    public virtual Reserva Reserva { get; set; } = null!;
}
