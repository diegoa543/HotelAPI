using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class ActivarHotel
{
    public class ActivarHotelCommand : IRequest<Hotel>
    {
        public int? Id { get; set; }
    }
    public class ActivarHotelCommandValidation : AbstractValidator<ActivarHotelCommand>
    {
        public ActivarHotelCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class ActivarHotelHandler : IRequestHandler<ActivarHotelCommand, Hotel>
    {
        private readonly ActivarHotelCommandValidation _validation;
        private readonly IActivarHotel _activarHotel;
        private readonly IValidarToken _validarToken;

        public ActivarHotelHandler(ActivarHotelCommandValidation validation, IActivarHotel activarHotel, IValidarToken validarToken)
        {
            _validation = validation;
            _activarHotel = activarHotel;
            _validarToken = validarToken;
        }
        public async Task<Hotel> Handle(ActivarHotelCommand command,CancellationToken cancellationToken)
        {
            _validation.Validate(command);
            var attributes = typeof(ActivarHotel).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var hotel = _activarHotel.ActiveHotel(command.Id);
            return await hotel;
        }
    }
}
