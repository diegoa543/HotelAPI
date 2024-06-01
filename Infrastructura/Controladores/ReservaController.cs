using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HOTEL_API.Aplicacion.Mediadores.Commands.AgregarReserva;
using static HOTEL_API.Aplicacion.Mediadores.Queries.BuscarAlojamiento;
using static HOTEL_API.Aplicacion.Mediadores.Queries.ListarReservas;

namespace HOTEL_API.Infrastructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("BuscarAlojamiento")]
    [Authorize]
    public async Task<ActionResult<List<Habitacion>>> BuscarAlojamiento(DateTime fechaInicio, DateTime fechaFin, int cantidadPersonas, string ciudad)
    {
        try
        {
            return Ok(await _mediator.Send(new BuscarAlojamientoQuery { NombreCiudad = ciudad, FechaInicio = fechaInicio, FechaFin = fechaFin, CantPersonas = cantidadPersonas }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPost]
    [Route("CrearReserva")]
    [Authorize]
    public async Task<IActionResult> CreateReserva([FromBody] ReservaCommand reservaCommand)
    {
        try
        {
            return Ok(await _mediator.Send(reservaCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpGet]
    [Route("BuscarReservas")]
    [Authorize]
    public async Task<ActionResult<List<ReservaDto>>> GetReservas()
    {
        try
        {
            return Ok(await _mediator.Send(new ListarReservasQuery()));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }
}
