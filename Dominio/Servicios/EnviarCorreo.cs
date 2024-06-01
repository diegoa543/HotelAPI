using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace HOTEL_API.Dominio.Servicios;

public class EnviarCorreo : IEmailService
{
    private readonly DbHotelContext _context;

    public EnviarCorreo(DbHotelContext context)
    {
        _context = context;
    }

    public async Task<EnviarCorreoDTO> EnviarCorreoCliente(int? clienteId, int reservaId)
    {
        var cliente = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == clienteId);
        //var reserva = await _context.Reservas.FirstOrDefaultAsync(x => x.Id == reservaId);
        var result = await (from reserva in _context.Reservas
                            join habitacionReserva in _context.HabitacionReservas on reserva.Id equals habitacionReserva.ReservaId
                            join habitacion in _context.Habitacions on habitacionReserva.HabitacionId equals habitacion.Id
                            join hotel in _context.Hotels on habitacion.HotelId equals hotel.Id
                            where reserva.Id == reservaId
                            select new ReservaInfoDto
                            {
                                HotelNombre = hotel.Nombre,
                                Direccion = hotel.Direccion,
                                FechaInicio = reserva.FechaInicio.Value,
                                FechaFin = reserva.FechaFin.Value,
                                HabitacionNumero = habitacion.Numero.Value,
                                TipoHabitacion = habitacion.TipoHabitacion
                            }).FirstOrDefaultAsync();

        var email = new MimeMessage();
        if (cliente != null && result != null)
        {
            email.From.Add(new MailboxAddress("Gerente", "pruebaapi59@gmail.com"));
            email.To.Add(new MailboxAddress(cliente.Nombre, cliente.Email));
            email.Subject = "Confirmación de Reserva";
            email.Body = new TextPart("plain")
            {
                Text = $"Estimado/a {cliente.Nombre},\n" +
                $"¡Gracias por Elegir {result.HotelNombre} para tu próxima estancia! Estamos emocionados de recibirte y asegurarnos de que tengas una experiencia inolvidable.\n" +
                $"A continuación, te proporcionamos los detalles de tu reserva:\n" +
                $"\n\nNúmero de Reserva: {reservaId}\n\n" +
                $"\n\nTipo de Habitación: {result.TipoHabitacion}\n\n" +
                $"\n\nFechas de Estancia: Del {result.FechaInicio} al {result.FechaFin}\n\n" +
                $"\n\nDirección del Hotel: {result.Direccion}\n\n" +
                $"Check-in Online: Te animamos a realizar el check-in online antes de tu llegada. Esto agilizará el proceso en recepción y te permitirá disfrutar de más tiempo para explorar la ciudad.\r\n" +
                $"Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en responder a este correo.\r\n" +
                $"¡Esperamos verte pronto en el {result.HotelNombre}!\n" +
                $"Atentamente, El Equipo del {result.HotelNombre}\n"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("pruebaapi59@gmail.com", "qckx ntxu asay yydy");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            EnviarCorreoDTO enviarCorreoDTO = new EnviarCorreoDTO
            {
                ClienteId = clienteId,
                ReservaId = reservaId
            };

            return enviarCorreoDTO;
        }
        else { return null; }
    }
}
