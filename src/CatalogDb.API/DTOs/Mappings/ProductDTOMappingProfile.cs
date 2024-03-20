using AutoMapper;
using CatalogDb.API.Entities;

namespace CatalogDb.API.DTOs.Mappings
{
    public class ProductDTOMappingProfile : Profile
    {
        public ProductDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
