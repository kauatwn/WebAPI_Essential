using AutoMapper;
using CatalogDb.API.Entities;

namespace CatalogDb.API.DTOs.Mappings
{
    public class DTOsMappingProfile : Profile
    {
        public DTOsMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDTO>().ReverseMap();
        }
    }
}
