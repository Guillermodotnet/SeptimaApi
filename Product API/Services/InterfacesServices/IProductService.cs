// En esta línea importo el espacio de nombres donde tengo definida mi entidad Product.
// La necesito para que los métodos de mi interfaz puedan trabajar con objetos de tipo Product.
using Product_API.Models.Entities;

// Defino el namespace donde coloco todas mis interfaces de servicios.
// Aquí organizo las reglas o contratos que deben seguir las clases de implementación.
namespace Product_API.Services.InterfacesServices
{
    // Declaro mi interfaz IProductService.
    // Esta interfaz define las operaciones que cualquier servicio de productos debe implementar.
    // Me permite separar la definición de la lógica del negocio de su implementación real.
    public interface IProductService
    {
        // Declaro un método asincrónico que obtiene todos los productos de la base de datos.
        // Devuelve una lista de objetos Product envuelta en una Task, lo que me permite ejecutar consultas sin bloquear el hilo principal.
        Task<List<Product>> GetAllAsync();

        // Este método busca un producto específico por su Id.
        // El signo de interrogación (?) indica que puede devolver null si no se encuentra ningún producto con ese Id.
        Task<Product?> GetByIdAsync(int id);

        // Aquí defino el método que crea un nuevo producto.
        // Recibe un objeto Product, lo guarda en la base de datos y devuelve el mismo producto (ya con su Id asignado).
        Task<Product> CreateAsync(Product product);

        // Este método actualiza un producto existente.
        // Si el Id no existe en la base de datos, devolverá null.
        // Me sirve para modificar registros de forma controlada.
        Task<Product?> UpdateAsync(int id, Product product);

        // Por último, defino el método para eliminar un producto por su Id.
        // Devuelve un booleano: true si la eliminación fue exitosa, o false si el producto no se encontró.
        Task<bool> DeleteAsync(int id);
    }
}


