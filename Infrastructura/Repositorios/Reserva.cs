using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class Reserva
{
    public int Id { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? UsuarioId { get; set; }

    public string? NmContectoEmergencia { get; set; }

    public string? NumeroContacto { get; set; }

    public virtual ICollection<HabitacionReserva> HabitacionReservas { get; set; } = new List<HabitacionReserva>();

    public virtual ICollection<HuespedReserva> HuespedReservas { get; set; } = new List<HuespedReserva>();

    public virtual Usuario? Usuario { get; set; }
}
