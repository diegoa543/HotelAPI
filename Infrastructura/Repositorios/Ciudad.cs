using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class Ciudad
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? PaisId { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual Pai? Pais { get; set; }
}
