using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Commands;
[Perfil("1")]
public class AgregarHotel
{
    public class AgregarHotelCommand : IRequest<Hotel>
    {
        public string? NombreHotel { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }
    }
    public class AgregarHotelCommandValidation : AbstractValidator<AgregarHotelCommand>
    {
        public AgregarHotelCommandValidation()
        {
            RuleFor(x => x.NombreHotel).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Direccion).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Ciudad).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class AgregarHotelHandler : IRequestHandler<AgregarHotelCommand, Hotel>
    {
        private readonly AgregarHotelCommandValidation _validation;
        private readonly IInsertarHotel _insertarHotel;
        private readonly IValidarToken _validarToken;

        public AgregarHotelHandler(AgregarHotelCommandValidation validation, IInsertarHotel insertarHotel, IValidarToken validarToken)
        {
            _validation = validation;
            _insertarHotel = insertarHotel;
            _validarToken = validarToken;
        }
        public async Task<Hotel> Handle(AgregarHotelCommand command, CancellationToken cancellationToken)
        {
            _validation.Validate(command);
            var attributes = typeof(AgregarHotel).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var newHotel = _insertarHotel.InsertarHotel(command.NombreHotel, command.Direccion, command.Ciudad);
            return await newHotel;
        }
    }
}
