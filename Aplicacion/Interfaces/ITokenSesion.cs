using HOTEL_API.Infrastructura.Repositorios;
using System.Security.Claims;
namespace HOTEL_API.Aplicacion.Interfaces;

public interface ITokenSesion
{
    string CrearToken(Usuario userInfo, UsuariosPerfil usuariosPerfile);
    IEnumerable<Claim> ObtenerClaimsDeToken(string token);
    string ObtenerToken();
}
