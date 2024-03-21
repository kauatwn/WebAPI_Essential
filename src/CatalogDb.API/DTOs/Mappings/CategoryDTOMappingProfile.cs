using AutoMapper;
using CatalogDb.API.Entities;

namespace CatalogDb.API.DTOs.Mappings
{
    public class CategoryDTOMappingProfile : Profile
    {
        public CategoryDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
