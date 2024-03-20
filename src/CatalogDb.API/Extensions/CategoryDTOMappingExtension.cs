using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;

namespace CatalogDb.API.Extensions
{
    public static class CategoryDTOMappingExtension
    {
        public static IEnumerable<CategoryDTO> ToCategoriesDTOList(this IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                return [];
            }
            return categories.Select(category => new CategoryDTO(category.Id, category.Name, category.ImageUrl));
        }

        public static CategoryDTO? ToCategoryDTO(this Category category)
        {
            if (category != null)
            {
                return new CategoryDTO(category.Id, category.Name, category.ImageUrl);
            }
            return null;
        }

        public static Category? ToCategory(this CategoryDTO categoryDto)
        {
            if (categoryDto != null)
            {
                return new Category
                {
                    Id = categoryDto.Id,
                    Name = categoryDto.Name,
                    ImageUrl = categoryDto.ImageUrl
                };
            }
            return null;
        }
    }
}
