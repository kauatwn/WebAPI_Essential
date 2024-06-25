namespace CatalogDb.API.DTOs
{
    public record PaginationResultDTO<T>(IEnumerable<T> Items, PaginationMetadataDTO Metadata) where T : class;
}
