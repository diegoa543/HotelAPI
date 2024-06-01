using HOTEL_API.Aplicacion.Dtos;

namespace HOTEL_API.Aplicacion.Interfaces;
public interface IEmailService
{
    Task<EnviarCorreoDTO> EnviarCorreoCliente(int? clienteId, int reservaId);
}
