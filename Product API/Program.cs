using Microsoft.EntityFrameworkCore; // Necesario para configurar y usar Entity Framework Core
using Product_API.Data; // Contiene la clase AppDbContext que maneja la conexion con la base de datos
using Product_API.Mapper; // Contiene la configuracion de AutoMapper (MappingProfile)
using Product_API.Middleware; // Contiene el middleware de manejo de errores
using Product_API.Services.ImplementationServices; // Contiene la clase ProductService
using Product_API.Services.InterfacesServices; // Contiene la interfaz IProductService

// Crea el objeto builder que configura la aplicacion
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------
// Agregar servicios al contenedor de dependencias
// ---------------------------------------------------

// Agrega soporte para controladores (permite manejar rutas y peticiones HTTP)
builder.Services.AddControllers();

// Configura Swagger para generar documentacion automatica de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------------------------------
// Configuracion de la base de datos
// ---------------------------------------------------

// Obtiene la cadena de conexion definida en appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("Connection");

// Configura el contexto de base de datos (AppDbContext) con SQL Server
// De esta forma Entity Framework Core sabra como conectarse a la base de datos
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// ---------------------------------------------------
// Inyeccion de dependencias
// ---------------------------------------------------

// Registra el servicio IProductService con su implementacion ProductService
// AddScoped significa que se crea una nueva instancia del servicio por cada peticion HTTP
builder.Services.AddScoped<IProductService, ProductService>();

// ---------------------------------------------------
// Configuracion de AutoMapper
// ---------------------------------------------------

// Agrega y configura AutoMapper para transformar objetos entre DTOs y entidades
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>(); // Registra el perfil de mapeo
}, typeof(Program)); // Indica el assembly actual donde buscar configuraciones

// ---------------------------------------------------
// Construir la aplicacion
// ---------------------------------------------------

// Crea la aplicacion a partir del builder configurado
WebApplication app = builder.Build();

// ---------------------------------------------------
// Configuracion del pipeline de middleware
// ---------------------------------------------------

// Si la aplicacion esta en modo desarrollo, habilita Swagger y su interfaz grafica
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera la documentacion en formato JSON
    app.UseSwaggerUI(); // Muestra la interfaz visual de Swagger
}

// Agrega el middleware personalizado para manejar errores globalmente
app.UseMiddleware<ErrorHandlingMiddleware>();

// Redirige automaticamente peticiones HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita el sistema de autorizacion (se usa en los controladores con atributos [Authorize])
app.UseAuthorization();

// Mapea los controladores registrados a las rutas correspondientes
app.MapControllers();

// Inicia la aplicacion y comienza a escuchar peticiones HTTP
app.Run();
