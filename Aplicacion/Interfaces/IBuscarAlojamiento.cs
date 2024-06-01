using HOTEL_API.Infrastructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Aplicacion.Interfaces;

public interface IBuscarAlojamiento
{
    Task<List<Habitacion>> BuscarAlojamientoPorFecha(string? cuidad,DateTime? fechaInicio,DateTime? fechaFin, int? cantPersonas);
}
