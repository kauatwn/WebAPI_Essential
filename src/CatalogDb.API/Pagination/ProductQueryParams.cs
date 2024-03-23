namespace CatalogDb.API.Pagination
{
    public class ProductQueryParams
    {
        private const int _maxPageSize = 10;
        public int PageNumber { get; set; }
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }
    }
}