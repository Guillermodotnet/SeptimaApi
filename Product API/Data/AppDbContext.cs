// En esta línea importo la librería principal de Entity Framework Core,
// que necesito para poder interactuar con mi base de datos a través de clases y objetos.
using Microsoft.EntityFrameworkCore;

// Aquí importo el espacio de nombres donde tengo mis entidades, 
// en este caso la clase Product, que representa una tabla de mi base de datos.
using Product_API.Models.Entities;

namespace Product_API.Data
{
    // En esta parte defino mi clase AppDbContext, 
    // que representa la conexión a la base de datos y el punto de acceso para todas las operaciones de EF Core.
    // Hereda de DbContext, que es la clase base encargada de manejar el seguimiento, consultas y persistencia de datos.
    public class AppDbContext : DbContext
    {
        // Creo el constructor de la clase y le paso las opciones de configuración del contexto.
        // Estas opciones incluyen la cadena de conexión y el proveedor de base de datos (por ejemplo, SQL Server).
        // Normalmente, estas opciones las inyecto desde Program.cs mediante el contenedor de dependencias.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Aquí defino una propiedad DbSet de tipo Product.
        // Un DbSet representa una tabla dentro de la base de datos, 
        // y me permite realizar operaciones CRUD (crear, leer, actualizar, eliminar) sobre esa tabla.
        public DbSet<Product> Products { get; set; }
    }
}


