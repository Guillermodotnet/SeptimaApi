// En esta línea importo Entity Framework Core para poder usar sus métodos asincrónicos,
// como FindAsync y ToListAsync, que me permiten interactuar con la base de datos sin bloquear el hilo.
using Microsoft.EntityFrameworkCore;

// Aquí importo mi clase AppDbContext, que es el punto de acceso a la base de datos de mi aplicación.
using Product_API.Data;

// Importo la entidad Product, que representa la tabla de productos dentro de la base de datos.
using Product_API.Models.Entities;

// Finalmente, importo la interfaz IProductService, que es la que debo implementar en esta clase.
using Product_API.Services.InterfacesServices;

namespace Product_API.Services.ImplementationServices
{
    // Declaro mi clase ProductService, que implementa la interfaz IProductService.
    // Esta clase contiene toda la lógica para crear, leer, actualizar y eliminar productos.
    public class ProductService : IProductService
    {
        // Defino una variable privada y de solo lectura para almacenar mi contexto de base de datos.
        // Con esto puedo acceder a las tablas definidas en AppDbContext.
        private readonly AppDbContext _context;

        // En el constructor recibo el contexto de base de datos mediante inyección de dependencias.
        // Esto me permite trabajar con EF Core sin crear instancias manualmente.
        public ProductService(AppDbContext context)
        {
            // Asigno el contexto recibido al campo privado para usarlo en los demás métodos.
            _context = context;
        }

        // ---------------------------------------------------------
        // MÉTODO: Crear un nuevo producto
        // ---------------------------------------------------------
        public async Task<Product> CreateAsync(Product product)
        {
            // Agrego el nuevo producto al contexto, es decir, lo marco para inserción en la base de datos.
            await _context.Products.AddAsync(product);

            // Guardo los cambios de forma asincrónica para confirmar la inserción en la base de datos.
            await _context.SaveChangesAsync();

            // Devuelvo el producto recién creado (ahora con su Id generado por la base de datos).
            return product;
        }

        // ---------------------------------------------------------
        // MÉTODO: Eliminar un producto por su Id
        // ---------------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            // Busco el producto por su identificador. Si no existe, FindAsync devolverá null.
            var delete = await _context.Products.FindAsync(id);

            // Si el producto no se encontró, devuelvo false indicando que no se eliminó nada.
            if (delete == null)
            {
                return false;
            }

            // Si sí se encontró, lo marco para eliminación en el contexto.
            _context.Products.Remove(delete);

            // Guardo los cambios para aplicar la eliminación en la base de datos.
            await _context.SaveChangesAsync();

            // Devuelvo true para indicar que la eliminación fue exitosa.
            return true;
        }

        // ---------------------------------------------------------
        // MÉTODO: Obtener todos los productos
        // ---------------------------------------------------------
        public async Task<List<Product>> GetAllAsync()
        {
            // Uso ToListAsync() para obtener todos los productos de la base de datos
            // y convertirlos en una lista de forma asincrónica.
            return await _context.Products.ToListAsync();
        }

        // ---------------------------------------------------------
        // MÉTODO: Obtener un producto por su Id
        // ---------------------------------------------------------
        public async Task<Product?> GetByIdAsync(int id)
        {
            // Uso FindAsync para buscar el producto directamente por su clave primaria (Id).
            // Si no existe, devuelve null.
            return await _context.Products.FindAsync(id);
        }

        // ---------------------------------------------------------
        // MÉTODO: Actualizar un producto existente
        // ---------------------------------------------------------
        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            // Primero busco el producto existente en la base de datos.
            var existing = await _context.Products.FindAsync(id);

            // Si no se encuentra el producto, devuelvo null para indicar que no se pudo actualizar.
            if (existing == null)
                return null;

            // Si se encontró, actualizo las propiedades necesarias con los nuevos valores.
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Category = product.Category;
            existing.Price = product.Price;

            // Guardo los cambios en la base de datos de forma asincrónica.
            await _context.SaveChangesAsync();

            // Devuelvo el producto actualizado para confirmar el resultado.
            return existing;
        }
    }
}
