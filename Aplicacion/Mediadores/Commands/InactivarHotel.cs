using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Configuraciones;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class InactivarHotel
{
    public class InactivarHotelCommand : IRequest<Hotel>
    {
        public int? Id { get; set; }
    }
    public class InactivarHotelCommandValidation : AbstractValidator<InactivarHotelCommand>
    {
        public InactivarHotelCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class InactivarHotelHandle : IRequestHandler<InactivarHotelCommand, Hotel>
    {
        private readonly InactivarHotelCommandValidation _validation;
        private readonly IInactivarHotel _inactivarHotel;
        private readonly IValidarToken _validarToken;

        public InactivarHotelHandle(InactivarHotelCommandValidation validation, IInactivarHotel inactivarHotel, IValidarToken validarToken)
        {
            _validation = validation;
            _inactivarHotel = inactivarHotel;
            _validarToken = validarToken;
        }
        public async Task<Hotel> Handle(InactivarHotelCommand command,CancellationToken cancellationToken)
        {
            _validation.Validate(command);
            var attributes = typeof(InactivarHotel).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var hotel = _inactivarHotel.InactivateHotel(command.Id);
            return await hotel;
        }
    }
}
