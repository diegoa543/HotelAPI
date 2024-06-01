using HOTEL_API.Infrastructura.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HOTEL_API.Aplicacion.Mediadores.Commands.ActivarHabitacion;
using static HOTEL_API.Aplicacion.Mediadores.Commands.ActualizarHabitacion;
using static HOTEL_API.Aplicacion.Mediadores.Commands.AgregarHabitacion;
using static HOTEL_API.Aplicacion.Mediadores.Commands.InactivarHabitacion;

namespace HOTEL_API.Infrastructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class HabitacionController : ControllerBase
{
    private readonly IMediator _mediator;

    public HabitacionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("SaveRoom")]
    [Authorize]
    public async Task<ActionResult<Habitacion>> InsertarNuevaHabitacion([FromBody] AgregarHabitacionCommad agregarHabitacionCommad)
    {
        try
        {
            return Ok(await _mediator.Send(agregarHabitacionCommad));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("UpdateRoom")]
    [Authorize]
    public async Task<ActionResult<Habitacion>> ActHabitacion([FromBody] ActualizarHabitacionCommand actualizarHabitacionCommand)
    {
        try
        {
            return Ok(await _mediator.Send(actualizarHabitacionCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("ActivarRoom")]
    [Authorize]
    public async Task<ActionResult<Habitacion>> Activarabitacion(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new ActivarHabitacionCommand { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("InactivarRoom")]
    [Authorize]
    public async Task<ActionResult<Habitacion>> Inactivarabitacion(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new InactivarHabitacionCommand { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

}
