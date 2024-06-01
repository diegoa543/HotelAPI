using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class Huesped
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Genero { get; set; }

    public string? TipoDocu { get; set; }

    public string? Documento { get; set; }

    public string? TelefonoMovil { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<HuespedReserva> HuespedReservas { get; set; } = new List<HuespedReserva>();
}
