using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public class ValidarUsuarioPerfil : IUsuarioPerfil
{
    private DbHotelContext _context;
    private readonly ITokenSesion _token;

    public ValidarUsuarioPerfil(DbHotelContext context, ITokenSesion token)
    {
        _context = context;
        _token = token;
    }
    public async Task<Usuario?> GetUsuarioByEmailAndContraAsync(string email, string contra)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Contra == contra);
    }

    public async Task<UsuariosPerfil?> GetUsuarioPerfilByUsuarioIdAsync(int usuarioId)
    {
        return await _context.UsuariosPerfils.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
    }

    public async Task<TokenDto> ValidarUsuarioYPerfil(string email, string contra)
    {
        var usuario = await GetUsuarioByEmailAndContraAsync(email, contra);

        if (usuario != null)
        {
            var perfil = await GetUsuarioPerfilByUsuarioIdAsync(usuario.IdUsuario);

            if (perfil != null)
            {
                var tokenString = _token.CrearToken(usuario, perfil);
                TokenDto tk = new() { Token = tokenString };
                return tk;
            }
            else
            {
                throw new UnauthorizedAccessException("Perfil de usuario no encontrado");
            }
        }
        else
        {
            throw new UnauthorizedAccessException("Usuario no encontrado");
        }
    }
}
