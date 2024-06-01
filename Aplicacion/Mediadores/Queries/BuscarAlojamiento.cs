using FluentValidation;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Infrastructura.Repositorios;
using MediatR;

namespace HOTEL_API.Aplicacion.Mediadores.Queries
{
    public class BuscarAlojamiento
    {
        public class BuscarAlojamientoQuery : IRequest<List<Habitacion>>
        {
            public string? NombreCiudad { get; set; }

            public DateTime? FechaInicio { get; set; }

            public DateTime? FechaFin { get; set; }

            public int? CantPersonas { get; set; }
        }
        public class BuscarAlojamientoQueryValidation : AbstractValidator<BuscarAlojamientoQuery>
        {
            public BuscarAlojamientoQueryValidation()
            {
                RuleFor(x => x.NombreCiudad).Cascade(CascadeMode.Stop).NotEmpty();
                RuleFor(x => x.FechaInicio).Cascade(CascadeMode.Stop).NotEmpty();
                RuleFor(x => x.FechaInicio).Cascade(CascadeMode.Stop).NotEmpty();
                RuleFor(x => x.CantPersonas).Cascade(CascadeMode.Stop).NotEmpty();
            }
        }
        public class BuscarAlojamientoHandle : IRequestHandler<BuscarAlojamientoQuery, List<Habitacion>>
        {
            private readonly BuscarAlojamientoQueryValidation _validation;
            private readonly IBuscarAlojamiento _buscarAlojamiento;

            public BuscarAlojamientoHandle(BuscarAlojamientoQueryValidation validation, IBuscarAlojamiento buscarAlojamiento)
            {
                _validation = validation;
                _buscarAlojamiento = buscarAlojamiento;
            }
            public async Task<List<Habitacion>> Handle(BuscarAlojamientoQuery command, CancellationToken cancellationToken)
            {
                _validation.Validate(command);
                var busqueda = _buscarAlojamiento.BuscarAlojamientoPorFecha(command.NombreCiudad, command.FechaInicio, command.FechaFin, command.CantPersonas);
                return await busqueda;
            }
        }
    }
}
