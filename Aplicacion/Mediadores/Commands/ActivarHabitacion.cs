using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class ActivarHabitacion
{
    public class ActivarHabitacionCommand : IRequest<Habitacion>
    {
        public int? Id { get; set; }
    }
    public class ActivarHabitacionCommandValidation : AbstractValidator<ActivarHabitacionCommand>
    {
        public ActivarHabitacionCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class ActivarHabitacionHandle : IRequestHandler<ActivarHabitacionCommand, Habitacion>
    {
        private readonly ActivarHabitacionCommandValidation _validation;
        private readonly IActivarHabitacion _activarHabitacion;
        private readonly IValidarToken _validarToken;


        public ActivarHabitacionHandle(ActivarHabitacionCommandValidation validation, IActivarHabitacion activarHabitacion_, IValidarToken validarToken)
        {
            _validation = validation;
            _activarHabitacion = activarHabitacion_;
            _validarToken = validarToken;
        }

        public async Task<Habitacion> Handle(ActivarHabitacionCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(ActivarHabitacion).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var room = _activarHabitacion.ActivarRoom(request.Id);
            return await room;
        }
    }
}
