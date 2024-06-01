using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class Perfil
{
    public int IdPerfil { get; set; }

    public string? NombrePerfil { get; set; }

    public virtual ICollection<UsuariosPerfil> UsuariosPerfils { get; set; } = new List<UsuariosPerfil>();
}
