using HOTEL_API.Aplicacion.Dtos;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface ISaveUsuario
{
    Task<UsuarioDTO> SaveUsuario(string nombre, string mail, string contra, int? perfil);
}
