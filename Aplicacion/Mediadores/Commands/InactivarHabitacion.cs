using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class InactivarHabitacion
{
    public class InactivarHabitacionCommand : IRequest<Habitacion>
    {
        public int? Id { get; set; }
    }
    public class InactivarHabitacionCommandValidation : AbstractValidator<InactivarHabitacionCommand>
    {
        public InactivarHabitacionCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class InactivarHabitacionHandle : IRequestHandler<InactivarHabitacionCommand, Habitacion>
    {
        private readonly InactivarHabitacionCommandValidation _validation;
        private readonly IInactivarHabitacion _inactivarHabitacion;
        private readonly IValidarToken _validarToken;

        public InactivarHabitacionHandle(InactivarHabitacionCommandValidation validation, IInactivarHabitacion inactivarHabitacion, IValidarToken validarToken)
        {
            _validation = validation;
            _inactivarHabitacion = inactivarHabitacion;
            _validarToken = validarToken;
        }

        public async Task<Habitacion> Handle(InactivarHabitacionCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(InactivarHabitacion).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var room = _inactivarHabitacion.InactivarRoom(request.Id);
            return await room;
        }
    }
}
