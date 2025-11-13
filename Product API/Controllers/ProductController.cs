// En esta línea importo la librería AutoMapper, que me permite convertir fácilmente entre entidades y DTOs.
using AutoMapper;

// Aquí importo el espacio de nombres de ASP.NET Core MVC, 
// necesario para poder usar controladores, rutas, atributos y respuestas HTTP.
using Microsoft.AspNetCore.Mvc;

// En esta línea importo los DTOs que uso para enviar y recibir información del cliente.
using Product_API.Models.DTOs;

// Aquí importo mis entidades del modelo, en este caso la clase Product.
using Product_API.Models.Entities;

// Finalmente, importo la interfaz del servicio, que define las operaciones disponibles para los productos.
using Product_API.Services.InterfacesServices;

namespace Product_API.Controllers
{
    // Defino la ruta base de este controlador. 
    // Con esto, las peticiones a “api/Product” se dirigirán aquí automáticamente.
    [Route("api/[controller]")]

    // Uso el atributo [ApiController] para indicar que esta clase se comporta como un controlador de API,
    // lo que habilita características como la validación automática del modelo.
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Declaro un campo privado de sólo lectura para acceder a la lógica de negocio de productos.
        private readonly IProductService _services;

        // También declaro un campo privado para usar AutoMapper y manejar conversiones entre entidades y DTOs.
        private readonly IMapper _mapper;

        // En el constructor recibo las dependencias (servicio e IMapper) mediante inyección de dependencias.
        public ProductController(IProductService services, IMapper mapper)
        {
            // Asigno el servicio inyectado al campo privado para poder usarlo dentro de la clase.
            _services = services;

            // Asigno el mapeador inyectado al campo privado.
            _mapper = mapper;
        }

        // ---------------------------------------------------------------------
        // MÉTODO GET: api/Product
        // ---------------------------------------------------------------------

        // Este endpoint devuelve todos los productos registrados.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Indico que puede devolver 200 (éxito).
        public async Task<IActionResult> GetAll()
        {
            // Llamo al servicio para obtener todos los productos de la base de datos.
            var product = await _services.GetAllAsync();

            // Uso AutoMapper para convertir la lista de entidades en una lista de DTOs.
            var dtoproduct = _mapper.Map<List<ProductDTO>>(product);

            // Devuelvo la lista al cliente junto con un código 200 OK.
            return Ok(dtoproduct);
        }

        // ---------------------------------------------------------------------
        // MÉTODO GET: api/Product/{id}
        // ---------------------------------------------------------------------

        // Este endpoint obtiene un producto específico según su ID.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            // Llamo al servicio para buscar el producto por ID.
            var idproduct = await _services.GetByIdAsync(id);

            // Si no se encuentra, devuelvo un 404 Not Found.
            if (idproduct == null)
            {
                return NotFound();
            }

            // Si se encuentra, convierto la entidad en un DTO.
            var idDtoProduct = _mapper.Map<ProductDTO>(idproduct);

            // Devuelvo el producto encontrado con código 200 OK.
            return Ok(idDtoProduct);
        }

        // ---------------------------------------------------------------------
        // MÉTODO POST: api/Product
        // ---------------------------------------------------------------------

        // Este endpoint permite crear un nuevo producto en la base de datos.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // Indica creación exitosa.
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // En caso de datos inválidos.
        public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDTO)
        {
            // Primero convierto el DTO recibido en una entidad Product.
            var product = _mapper.Map<Product>(productDTO);

            // Llamo al servicio para agregarlo a la base de datos.
            var createdProduct = await _services.CreateAsync(product);

            // Luego convierto la entidad creada de nuevo a DTO.
            var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);

            // Finalmente, devuelvo un 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
        }

        // ---------------------------------------------------------------------
        // MÉTODO PUT: api/Product/{id}
        // ---------------------------------------------------------------------

        // Este endpoint permite actualizar un producto existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDto)
        {
            // Verifico que el ID enviado en la URL coincida con el del cuerpo de la petición.
            if (id != productDto.Id)
            {
                // Si no coinciden, devuelvo un 400 con un mensaje explicativo.
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la petición.");
            }

            // Convierto el DTO a entidad para poder trabajar con EF Core.
            var product = _mapper.Map<Product>(productDto);

            // Llamo al servicio para aplicar la actualización.
            var updatedProduct = await _services.UpdateAsync(id, product);

            // Si el producto no existe, devuelvo un 404 Not Found.
            if (updatedProduct == null)
            {
                return NotFound();
            }

            // Si la actualización fue exitosa, devuelvo un 204 No Content.
            return NoContent();
        }

        // ---------------------------------------------------------------------
        // MÉTODO DELETE: api/Product/{id}
        // ---------------------------------------------------------------------

        // Este endpoint elimina un producto de la base de datos según su ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Llamo al servicio para intentar eliminar el producto.
            var result = await _services.DeleteAsync(id);

            // Si el servicio devuelve false, significa que no se encontró el producto.
            if (!result)
            {
                return NotFound();
            }

            // Si se eliminó correctamente, devuelvo un 204 No Content.
            return NoContent();
        }
    }
}
