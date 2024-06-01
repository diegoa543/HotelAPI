using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;

namespace HOTEL_API.Infrastructura.Repositorios;

public class GuardarUsuario : ISaveUsuario
{
    private readonly DbHotelContext _context;

    public GuardarUsuario(DbHotelContext context)
    {
        _context = context;
    }
    public async Task<UsuarioDTO> SaveUsuario(string nombre, string mail, string contra, int? perfil)
    {
        Usuario usu = new Usuario
        {
            Nombre = nombre,
            Email = mail,
            Contra = contra
        };
        await _context.Usuarios.AddAsync(usu);
        await _context.SaveChangesAsync();
        if (usu == null)
            throw new ArgumentNullException(nameof(usu));

        UsuariosPerfil perfilUsu = new UsuariosPerfil
        {
            IdUsuario = usu.IdUsuario,
            IdPerfil = perfil
        };
        await _context.UsuariosPerfils.AddAsync(perfilUsu);
        await _context.SaveChangesAsync();
        if (perfil == null)
            throw new ArgumentNullException(nameof(perfil));

        UsuarioDTO usuarioPerfil = new UsuarioDTO
        {
            Nombre = usu.Nombre,
            Email = usu.Email,
            Contraseña = usu.Contra,
            Perfil = perfilUsu.IdPerfil
        };

        return usuarioPerfil;
    }
}
