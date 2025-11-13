// En esta línea importo el espacio de nombres que me permite usar atributos de validación,
// como [Required], [MaxLength], [Key], etc. Estos atributos me ayudan a validar los datos del cliente.
using System.ComponentModel.DataAnnotations;

// Importo otro espacio de nombres que me deja especificar cómo se mapean los datos a la base de datos,
// por ejemplo, el tipo de columna o su precisión decimal.
using System.ComponentModel.DataAnnotations.Schema;

// Defino el namespace donde coloco mi clase DTO (Data Transfer Object).
// Esto me ayuda a mantener mi proyecto organizado dentro de la carpeta Models/DTOs.
namespace Product_API.Models.DTOs
{
    // Declaro la clase ProductDTO, que usaré como un “puente” entre el cliente y mi API.
    // El objetivo de este DTO es recibir y enviar datos de productos sin exponer directamente mi entidad Product.
    public class ProductDTO
    {
        // Atributo [Key]: Indico que esta propiedad representa el identificador del producto.
        // Aunque en los DTO no siempre es obligatorio, lo incluyo para mantener coherencia con la entidad.
        public int Id { get; set; }

        // Atributo [Required]: Indico que el campo “Name” es obligatorio.
        // Si el cliente no lo envía, el modelo no pasará la validación.
        // También incluyo un mensaje de error personalizado.
        [Required(ErrorMessage = "El nombre es obligatorio")]

        // Atributo [MaxLength]: Limito la cantidad máxima de caracteres que el cliente puede enviar (50).
        // Esto previene datos innecesariamente largos.
        [MaxLength(50)]

        // Declaro la propiedad “Name” como string y la inicializo con null-forgiving (!) 
        // para evitar advertencias de nulabilidad.
        public string Name { get; set; } = null!; // Nombre del producto

        // Requiero que la descripción también sea obligatoria.
        // Si el cliente intenta crear o actualizar un producto sin descripción, la API lo rechazará.
        [Required(ErrorMessage = "La descripción es obligatoria")]

        // Establezco una longitud máxima de 300 caracteres para la descripción.
        [MaxLength(300)]

        // Defino la propiedad Description, donde guardo la descripción del producto.
        public string Description { get; set; } = null!;

        // También hago que la categoría sea obligatoria.
        // Así garantizo que cada producto tenga una clasificación definida.
        [Required(ErrorMessage = "La categoria es obligatoria")]

        // Límite de longitud para evitar nombres de categoría demasiado largos.
        [MaxLength(50)]

        // Propiedad Category: Indica el tipo o clasificación del producto.
        public string Category { get; set; } = null!;

        // El precio también debe ser obligatorio.
        // Uso un mensaje de error personalizado para que el cliente sepa qué campo falta.
        [Required(ErrorMessage = "El precio es obligatorio")]

        // Aquí defino cómo se debe almacenar el precio en la base de datos.
        // "decimal(18,2)" significa que puede tener hasta 18 dígitos en total y 2 decimales.
        [Column(TypeName = "decimal(18,2)")]

        // Propiedad Price: Representa el costo del producto.
        public decimal Price { get; set; }

        // El stock (cantidad disponible) también debe ser obligatorio.
        // Si no se proporciona, la validación fallará.
        [Required(ErrorMessage = "El stock es obligatorio")]

        // Propiedad Stock: Indica cuántas unidades del producto tengo disponibles.
        public int Stock { get; set; }
    }
}