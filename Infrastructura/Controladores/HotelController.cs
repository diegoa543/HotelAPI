using HOTEL_API.Infrastructura.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HOTEL_API.Aplicacion.Mediadores.Commands.ActivarHotel;
using static HOTEL_API.Aplicacion.Mediadores.Commands.ActualizarHotel;
using static HOTEL_API.Aplicacion.Mediadores.Commands.AgregarHotel;
using static HOTEL_API.Aplicacion.Mediadores.Commands.InactivarHotel;

namespace HOTEL_API.Infrastructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("SaveHotel")]
    [Authorize]
    public async Task<ActionResult<Hotel>> InsertarNuevoHotel([FromBody] AgregarHotelCommand agregarHotelCommand)
    {
        try
        {
            return Ok(await _mediator.Send(agregarHotelCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("UpdateRoom")]
    [Authorize]
    public async Task<ActionResult<Hotel>> ActHotel([FromBody] ActualizarHotelCommad actualizarHotelCommad)
    {
        try
        {
            return Ok(await _mediator.Send(actualizarHotelCommad));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("ActivarEstadoHotel")]
    [Authorize]
    public async Task<ActionResult<Hotel>> ActivarEstadoHotel(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new ActivarHotelCommand { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("InactivarEstadoHotel")]
    [Authorize]
    public async Task<ActionResult<Hotel>> InactivarEstadoHotel(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new InactivarHotelCommand { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }
}
