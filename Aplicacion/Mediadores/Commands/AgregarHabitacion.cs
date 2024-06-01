using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class AgregarHabitacion
{
    public class AgregarHabitacionCommad : IRequest<Habitacion>
    {
        public int? Numero { get; set; }

        public int? Piso { get; set; }

        public int? CostoBase { get; set; }

        public int? Impuestos { get; set; }

        public string? TipoHabitacion { get; set; }

        public int? CantPersonas { get; set; }

        public string? Ubicacion { get; set; }

        public string? Hotel { get; set; }
    }
    public class AgregarHabitacionCommadValidation : AbstractValidator<AgregarHabitacionCommad>
    {
        public AgregarHabitacionCommadValidation()
        {
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
    public class AgregarHabitacionHandle : IRequestHandler<AgregarHabitacionCommad, Habitacion>
    {
        private readonly AgregarHabitacionCommadValidation _validator;
        private readonly IInsertarHabitacion _insertarHabitacion;
        private readonly IValidarToken _validarToken;

        public AgregarHabitacionHandle(AgregarHabitacionCommadValidation validator, IInsertarHabitacion insertarHabitacion, IValidarToken validarToken)
        {
            _validator = validator;
            _insertarHabitacion = insertarHabitacion;
            _validarToken = validarToken;
        }
        public async Task<Habitacion> Handle(AgregarHabitacionCommad commad,CancellationToken cancellationToken)
        {
            _validator.Validate(commad);
            var attributes = typeof(AgregarHabitacion).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var newRoom = _insertarHabitacion.InsertarHabitacion(commad.Numero, commad.Piso, commad.CostoBase, commad.Impuestos, commad.TipoHabitacion, commad.CantPersonas, commad.Ubicacion, commad.Hotel);
            return await newRoom;
        }
    }
}
