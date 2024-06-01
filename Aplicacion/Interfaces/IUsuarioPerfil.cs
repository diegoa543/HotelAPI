using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Infrastructura.Repositorios;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IUsuarioPerfil
{
    Task<Usuario?> GetUsuarioByEmailAndContraAsync(string email, string contra);
    Task<UsuariosPerfil?> GetUsuarioPerfilByUsuarioIdAsync(int usuarioId);
    Task<TokenDto> ValidarUsuarioYPerfil(string email, string contra);
}
