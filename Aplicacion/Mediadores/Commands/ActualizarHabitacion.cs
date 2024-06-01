using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class ActualizarHabitacion
{
    public class ActualizarHabitacionCommand : IRequest<Habitacion>
    {
        public int? Id { get; set; }

        public int? Numero { get; set; }

        public int? Piso { get; set; }

        public int? CostoBase { get; set; }

        public int? Impuestos { get; set; }

        public string? TipoHabitacion { get; set; }

        public int? CantPersonas { get; set; }

        public string? Ubicacion { get; set; }

        public string? Hotel { get; set; }
    }
    public class ActualizarHabitacionCommandValidation : AbstractValidator<ActualizarHabitacionCommand>
    {
        public ActualizarHabitacionCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Numero).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Piso).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.CostoBase).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Impuestos).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.TipoHabitacion).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.CantPersonas).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Ubicacion).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Hotel).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class ActualizarHabitacionHandler : IRequestHandler<ActualizarHabitacionCommand, Habitacion>
    {
        private readonly ActualizarHabitacionCommandValidation _validator;
        private readonly IActualizarHabitacion _actualizarHabitacion;
        private readonly IValidarToken _validarToken;

        public ActualizarHabitacionHandler(ActualizarHabitacionCommandValidation validator, IActualizarHabitacion actualizarHabitacion, IValidarToken validarToken)
        {
            _validator = validator;
            _actualizarHabitacion = actualizarHabitacion;
            _validarToken = validarToken;
        }
        public async Task<Habitacion> Handle(ActualizarHabitacionCommand command,CancellationToken cancellationToken)
        {
            _validator.Validate(command);
            var attributes = typeof(ActualizarHabitacion).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var room = _actualizarHabitacion
                        .UpdateHabitacionExistente(command.Id, command.Numero, command.Piso, command.CostoBase, command.Impuestos, 
                                                   command.TipoHabitacion, command.CantPersonas, command.Ubicacion, command.Hotel);
            return await room;

        }
    }
}
