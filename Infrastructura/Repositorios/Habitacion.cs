using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class Habitacion
{
    public int Id { get; set; }

    public int? Numero { get; set; }

    public int? Piso { get; set; }

    public int? CostoBase { get; set; }

    public int? Impuestos { get; set; }

    public string? TipoHabitacion { get; set; }

    public int? CantPersonas { get; set; }

    public string? Ubicacion { get; set; }

    public int? Activa { get; set; }

    public int? HotelId { get; set; }

    public virtual ICollection<HabitacionReserva> HabitacionReservas { get; set; } = new List<HabitacionReserva>();

    public virtual Hotel? Hotel { get; set; }
}
