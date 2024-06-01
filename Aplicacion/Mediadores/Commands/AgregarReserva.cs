using FluentValidation;
using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands
{
    public class AgregarReserva
    {
        public class ReservaCommand : IRequest<int>
        {
            public ReservaDto ReservaDto { get; set; }
        }
        public class ReservaCommandValidation : AbstractValidator<ReservaCommand>
        {
            public ReservaCommandValidation()
            {
                RuleFor(x => x.ReservaDto).Cascade(CascadeMode.Stop).NotEmpty();

                RuleFor(x => x.ReservaDto.Huespedes)
                    .NotEmpty().WithMessage("Debe haber al menos un huésped en la reserva.")
                    .ForEach(huesped =>
                    {
                        huesped.SetValidator(new HuespedValidator());
                    });
            }
        }
        public class HuespedValidator : AbstractValidator<Huesped>
        {
            public HuespedValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Genero).NotEmpty();
                RuleFor(x => x.TipoDocu).NotEmpty();
                RuleFor(x => x.Documento).NotEmpty();
                RuleFor(x => x.TelefonoMovil).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
        public class ReservaHandler : IRequestHandler<ReservaCommand, int>
        {
            private readonly ReservaCommandValidation _validation;
            private readonly IReserva _reserva;
            private readonly IEmailService _emailService;

            public ReservaHandler(ReservaCommandValidation validation, IReserva reserva, IEmailService emailService)
            {
                _validation = validation;
                _reserva = reserva;
                _emailService = emailService;
            }

            public async Task<int> Handle(ReservaCommand command, CancellationToken cancellationToken)
            {
                _validation.Validate(command);
                var reservaDto = command.ReservaDto;

                // Crear la reserva
                var reserva = new Reserva
                {
                    FechaInicio = reservaDto.FechaInicio,
                    FechaFin = reservaDto.FechaFin,
                    NmContectoEmergencia = reservaDto.NmContectoEmergencia,
                    NumeroContacto = reservaDto.NumeroContacto,
                    UsuarioId = reservaDto.UsuarioId
                };

                // Crear lista de huéspedes
                var huespedes = new List<Huesped>();
                foreach (var huespedDto in reservaDto.Huespedes)
                {
                    var huesped = new Huesped
                    {
                        Nombre = huespedDto.Nombre,
                        Apellidos = huespedDto.Apellidos,
                        Genero = huespedDto.Genero,
                        TipoDocu = huespedDto.TipoDocu,
                        Documento = huespedDto.Documento,
                        TelefonoMovil = huespedDto.TelefonoMovil,
                        Email = huespedDto.Email
                    };

                    huespedes.Add(huesped);
                }

                // Guardar reserva, huéspedes y habitaciones en una sola transacción
                var reservaId = await _reserva.AddReservaAsync(reserva, huespedes, reservaDto.HabitacionIds);

                // Enviar email de confirmación
                await _emailService.EnviarCorreoCliente(reserva.UsuarioId,reservaId);

                return reservaId;
            }
        }
    }
}
