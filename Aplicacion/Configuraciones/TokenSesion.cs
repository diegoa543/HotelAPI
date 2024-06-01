using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HOTEL_API.Aplicacion.Configuraciones;

public class TokenSesion : ITokenSesion
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenSesion(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Crear Token
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="usuariosPerfile"></param>
    /// <returns></returns>
    public string CrearToken(Usuario userInfo, UsuariosPerfil usuariosPerfile)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        IEnumerable<Claim> claims = new List<Claim> {
                new Claim("correo", userInfo.Email),
                new Claim("nombre", userInfo.Nombre),
                new Claim("perfil", usuariosPerfile.IdPerfil.ToString()??string.Empty)

            };
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
          _configuration["Jwt:Issuer"],
          claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Obtener token
    /// </summary>
    /// <returns></returns>
    public string ObtenerToken()
    {
        var scopes = new string[] { _configuration["Scope"] };
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        return token;
    }

    /// <summary>
    /// Obtener Claims
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public IEnumerable<Claim> ObtenerClaimsDeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        return jwtToken.Claims;
    }
}
