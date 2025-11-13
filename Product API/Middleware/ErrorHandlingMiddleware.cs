
// En esta línea defino el espacio de nombres (namespace) donde coloco mi middleware personalizado.
// Lo mantengo dentro de una carpeta llamada "Middleware" para tener el proyecto bien organizado.
namespace Product_API.Middleware
{
    // Aquí defino mi clase ErrorHandlingMiddleware.
    // La uso para capturar y manejar globalmente cualquier error que ocurra durante el procesamiento de una petición HTTP.
    public class ErrorHandlingMiddleware
    {
        // Declaro una variable privada y de solo lectura (_next) que representa al siguiente middleware en la cadena de ejecución.
        // Básicamente, este delegado es el que permite continuar el flujo hacia el siguiente componente.
        private readonly RequestDelegate _next;

        // También declaro un logger (registrador de eventos) para poder registrar errores o mensajes en consola o archivos.
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        // En este constructor recibo por inyección de dependencias tanto el "siguiente middleware" como el "logger".
        // De esta forma, no tengo que crearlos manualmente; el framework me los proporciona automáticamente.
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;   // Asigno el middleware siguiente al campo privado.
            _logger = logger; // Asigno el servicio de logging al campo privado.
        }

        // Este es el método principal que se ejecuta con cada solicitud HTTP que entra a la aplicación.
        // ASP.NET Core llama este método automáticamente al pasar por mi middleware.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Dentro del bloque try, intento ejecutar el siguiente middleware o controlador de la cadena.
                // Si todo funciona correctamente, la ejecución continuará normalmente sin lanzar excepciones.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción en cualquier parte del proceso (en otro middleware o en un controlador),
                // esa excepción será capturada aquí dentro del bloque catch.

                // Primero, registro el error usando el logger.
                // Esto me ayuda a tener un historial de errores que puedo consultar en consola o archivos de log.
                _logger.LogError(ex, "Ocurrió un error no controlado");

                // Luego, preparo la respuesta estándar que quiero devolver al cliente.
                // Establezco el código de estado HTTP en 500, que significa "Error interno del servidor".
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Defino que la respuesta se enviará en formato JSON.
                context.Response.ContentType = "application/json";

                // Creo un objeto anónimo con los datos que quiero devolver al cliente.
                // Incluyo una bandera de éxito (false), un mensaje genérico y los detalles del error.
                // El campo "detail" es muy útil cuando estoy depurando.
                var response = new
                {
                    success = false,
                    message = "Ha ocurrido un error en el servidor", // Mensaje genérico para no exponer demasiada información al usuario.
                    detail = ex.Message // Incluyo el detalle de la excepción (ideal en entornos de desarrollo).
                };

                // Finalmente, escribo la respuesta como JSON y la envío al cliente.
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}



