using FluentValidation;
using HOTEL_API.Aplicacion.Atributos;
using HOTEL_API.Aplicacion.Dtos;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Aplicacion.Mediadores.Commands;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Queries;
[Perfil("1")]
public class ListarReservas
{
    public class ListarReservasQuery : IRequest<IEnumerable<ListaReservasDto>>
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreHotel { get; set; }
        public int NumeroHabitacion { get; set; }
        public string? TipoHabitacion { get; set; }
    }
    public class ListarReservasQueryValidation : AbstractValidator<ListarReservasQuery>
    {
        public ListarReservasQueryValidation()
        {
            RuleFor(x => x.FechaInicio).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.FechaFin).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.NombreUsuario).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.NombreHotel).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.NumeroHabitacion).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.TipoHabitacion).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class ListarReservasHandle : IRequestHandler<ListarReservasQuery, IEnumerable<ListaReservasDto>>
    {
        private readonly ListarReservasQueryValidation _validation;
        private readonly IListarReservas _listarReservas;
        private readonly IValidarToken _validarToken;

        public ListarReservasHandle(ListarReservasQueryValidation validation, IListarReservas listarReservas, IValidarToken validarToken)
        {
            _validation = validation;
            _listarReservas = listarReservas;
            _validarToken = validarToken;
        }
        public async Task<IEnumerable<ListaReservasDto>> Handle(ListarReservasQuery request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(ListarReservas).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var listaReservas = _listarReservas.ListarReservasAsync();
            return await listaReservas;
        }
    }
}
