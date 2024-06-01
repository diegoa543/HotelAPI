using HOTEL_API.Aplicacion.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HOTEL_API.Aplicacion.Mediadores.Commands.SaveUsuario;

namespace HOTEL_API.Infrastructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("save")]
    [Authorize]
    public async Task<ActionResult<UsuarioDTO>> SaveUsuario([FromBody] SaveUsuarioCommand saveUsuarioCommand)
    {
        try
        {
            return Ok(await _mediator.Send(saveUsuarioCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }
}
