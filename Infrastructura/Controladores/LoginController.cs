using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Mediadores.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("iniciarSesion")]
    public async Task<ActionResult<TokenDto>> IniciarSesion([FromBody] LoginQuery loginQuery)
    {
        try
        {
            var tokenDto = await _mediator.Send(loginQuery);
            return Ok(tokenDto);
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Ocurrió un error al actualizar la base de datos");
        }
        catch (Exception ex)
        {
            // Registrar detalles del error
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud de inicio de sesión", message = ex.Message });
        }
    }
}
