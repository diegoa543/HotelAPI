using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class ActualizarHotel
{
    public class ActualizarHotelCommad : IRequest<Hotel>
    {
        public int? Id { get; set; }

        public string? NombreHotel { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }
    }
    public class ActualizarHotelCommadValidation : AbstractValidator<ActualizarHotelCommad>
    {
        public ActualizarHotelCommadValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.NombreHotel).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Direccion).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Ciudad).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class ActualizarHotelHandle : IRequestHandler<ActualizarHotelCommad, Hotel>
    {
        private readonly ActualizarHotelCommadValidation _validation;
        private readonly IActualizarHotel _actualizarHotel;
        private readonly IValidarToken _validarToken;

        public ActualizarHotelHandle(ActualizarHotelCommadValidation validation, IActualizarHotel actualizarHotel, IValidarToken validarToken)
        {
            _validation = validation;
            _actualizarHotel = actualizarHotel;
            _validarToken = validarToken;
        }
        public async Task<Hotel> Handle(ActualizarHotelCommad command,CancellationToken cancellationToken)
        {
            _validation.Validate(command);
            var attributes = typeof(ActualizarHotel).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var hotel = _actualizarHotel.UpdateHotelExistente(command.Id,command.NombreHotel,command.Direccion,command.Ciudad);
            return await hotel;
        }
    }
}
