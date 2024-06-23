namespace CatalogDb.API.DTOs
{
    public record PaginationResultDTO<T> where T : class
    {
        public IEnumerable<T> Items { get; init; }
        public PaginationMetadataDTO Metadata { get; init; }

        public PaginationResultDTO(IEnumerable<T> items, PaginationMetadataDTO metadata)
        {
            Items = items;
            Metadata = metadata;
        }
    }
}
