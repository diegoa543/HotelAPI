﻿using System;
using System.Collections.Generic;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class UsuariosPerfil
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdPerfil { get; set; }

    public virtual Perfil? IdPerfilNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
