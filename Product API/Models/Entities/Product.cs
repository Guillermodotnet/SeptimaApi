// En esta línea importo el espacio de nombres que me permite usar atributos de validación
// como [Required], [MaxLength], [Key], [Phone], etc. Estos me ayudan a asegurar que los datos sean correctos
// antes de guardarlos o procesarlos.
using System.ComponentModel.DataAnnotations;

// Aquí importo otro espacio de nombres que me deja definir cómo se mapea mi clase con la base de datos,
// por ejemplo, para establecer el tipo de columna con [Column(TypeName = "decimal(18,2)")].
using System.ComponentModel.DataAnnotations.Schema;

// Defino el namespace donde coloco todas mis entidades. En este caso, dentro de la carpeta Models/Entities.
namespace Product_API.Models.Entities
{
    // Declaro mi clase "Product", que representa una entidad dentro de la base de datos.
    // Cada instancia de esta clase será un registro en la tabla "Products".
    public class Product
    {
        // Atributo [Key]: Indico que esta propiedad "Id" es la clave primaria de mi tabla.
        // Esto hace que EF Core la use como identificador único de cada producto.
        public int Id { get; set; }

        // Atributo [Required]: Digo que el campo "Name" no puede estar vacío o ser nulo.
        // Si el cliente intenta crear un producto sin nombre, la validación fallará.
        [Required(ErrorMessage = "El nombre es obligatorio")]

        // Atributo [MaxLength]: Limito el nombre del producto a 50 caracteres como máximo.
        // Esto ayuda a mantener consistencia y evita texto excesivo.
        [MaxLength(50)]

        // Propiedad Name: Almacena el nombre del producto.
        // Uso "null!" para indicar al compilador que confíe en que esta propiedad se inicializará correctamente.
        public string Name { get; set; } = null!;

        // También requiero que el campo "Description" sea obligatorio.
        [Required(ErrorMessage = "La descripción es obligatoria")]

        // Limito la descripción a un máximo de 300 caracteres.
        [MaxLength(300)]

        // Propiedad Description: Aquí guardo una breve explicación o detalle del producto.
        public string Description { get; set; } = null!;

        // Atributo [Required]: Indico que toda categoría debe tener un valor.
        // Esto me ayuda a mantener los productos organizados por tipo.
        [Required(ErrorMessage = "La categoria es obligatoria")]

        // Limito la categoría a 50 caracteres.
        [MaxLength(50)]

        // Propiedad Category: Define el grupo o tipo de producto al que pertenece.
        public string Category { get; set; } = null!;

        // Hago que el campo Price sea obligatorio, ya que ningún producto debería existir sin un precio.
        [Required(ErrorMessage = "El precio es obligatorio")]

        // Especifico que la columna en la base de datos debe ser de tipo decimal(18,2),
        // lo que permite manejar precios con dos decimales y buena precisión.
        [Column(TypeName = "decimal(18,2)")]

        // Propiedad Price: Representa el valor monetario del producto.
        public decimal Price { get; set; }

        // Atributo [Required]: Indico que el stock también debe tener un valor.
        // No se permiten productos sin cantidad disponible definida.
        [Required(ErrorMessage = "El stock es obligatorio")]

        // Propiedad Stock: Almacena la cantidad de unidades disponibles de este producto.
        public int Stock { get; set; }

        // --- Datos opcionales del proveedor ---
        // Atributo [MaxLength]: Defino que el nombre del proveedor no puede exceder los 50 caracteres.
        [MaxLength(50)]

        // Propiedad SupplierName: Permito que sea nula, ya que no todos los productos tendrán proveedor asignado.
        public string? SupplierName { get; set; }

        // Atributo [Phone]: Valido que el valor tenga formato de teléfono válido.
        // Aunque la propiedad está como int, normalmente este atributo se usa con string.
        [Phone(ErrorMessage = "Número de teléfono no válido")]

        // Propiedad SupplierNumber: Guardo el número telefónico del proveedor.
        public int SupplierNumber { get; set; }

        // Atributo [EmailAddress]: Verifica que el texto tenga formato válido de correo electrónico.
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]

        // Propiedad SupplierEmail: Permito que sea nula, ya que el correo del proveedor es opcional.
        public string? SupplierEmail { get; set; }
    }
}
