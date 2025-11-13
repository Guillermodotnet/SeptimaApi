// En esta línea importo la librería AutoMapper, 
// que me permite convertir fácilmente entre diferentes tipos de objetos (por ejemplo, entre entidades y DTOs).
using AutoMapper;

// Aquí importo mis DTOs, que son las clases que uso para enviar y recibir datos a través de la API.
using Product_API.Models.DTOs;

// También importo mis entidades, que representan las tablas reales en la base de datos.
using Product_API.Models.Entities;

namespace Product_API.Mapper
{
    // Defino mi clase MappingProfile, que hereda de la clase Profile.
    // Esta clase se utiliza para registrar las reglas de conversión (mapeo) entre mis modelos y mis DTOs.
    public class MappingProfile : Profile
    {
        // En el constructor defino las configuraciones de mapeo que quiero que AutoMapper conozca.
        public MappingProfile()
        {
            // ------------------------------------------------------------------
            // De Entidad a DTO
            // ------------------------------------------------------------------

            // En esta línea le indico a AutoMapper que quiero poder convertir (mapear)
            // un objeto de tipo Product (mi entidad de base de datos)
            // a un objeto de tipo ProductDTO (el modelo que envío al cliente).
            CreateMap<Product, ProductDTO>();

            // ------------------------------------------------------------------
            // De DTO a Entidad
            // ------------------------------------------------------------------

            // Aquí hago lo contrario: defino que también quiero poder convertir
            // un objeto ProductDTO (recibido desde el cliente, por ejemplo, en un POST o PUT)
            // a un objeto Product (que es el que guardo en la base de datos).
            CreateMap<ProductDTO, Product>();
        }
    }
}


