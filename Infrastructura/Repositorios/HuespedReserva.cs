using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class HuespedReserva
{
    public int Id { get; set; }

    public int HuespedId { get; set; }

    public int ReservaId { get; set; }

    public virtual Huesped Huesped { get; set; } = null!;

    public virtual Reserva Reserva { get; set; } = null!;
}
