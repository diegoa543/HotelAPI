using FluentValidation.AspNetCore;
using HOTEL_API.Aplicacion.Configuraciones;
using HOTEL_API.Aplicacion.Interfaces;
using HOTEL_API.Dominio.Servicios;
using HOTEL_API.Infrastructura.Repositorios;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureServices();
builder.Services.AddDbContext<DbHotelContext>();
builder.Services.AddTransient<IInsertarHotel, PostNewHotel>();
builder.Services.AddTransient<IInsertarHabitacion, PostNewRoom>();
builder.Services.AddTransient<IActualizarHabitacion, UpdateRoom>();
builder.Services.AddTransient<IActualizarHotel, UpdateHotel>();
builder.Services.AddTransient<IActivarHotel, ActivarHotelPost>();
builder.Services.AddTransient<IEstadoHotel, EstadoHotelService>();
builder.Services.AddTransient<IInactivarHotel, InactivarHotelPost>();
builder.Services.AddTransient<IEstadoHabitacion, EstadoHabitacionServices>();
builder.Services.AddTransient<IActivarHabitacion, ActivarHabitacionPost>();
builder.Services.AddTransient<IInactivarHabitacion, InactivarHabitacionPost>();
builder.Services.AddTransient<IBuscarAlojamiento, BuscarAlojamientoGet>();
builder.Services.AddTransient<IReserva,AddReserva>();
builder.Services.AddTransient<IEmailService,EnviarCorreo>();
builder.Services.AddTransient<ITokenSesion, TokenSesion>();
builder.Services.AddTransient<IValidarToken, ValidarToken>();
builder.Services.AddTransient<IUsuarioPerfil, ValidarUsuarioPerfil>();
builder.Services.AddTransient<ISaveUsuario, GuardarUsuario>();
builder.Services.AddTransient<IListarReservas, GetReservas>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
    // Aquí puedes agregar otros proveedores de registro si lo deseas
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
                      Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
