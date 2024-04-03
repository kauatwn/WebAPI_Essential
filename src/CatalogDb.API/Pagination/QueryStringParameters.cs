namespace CatalogDb.API.Pagination
{
    public abstract class QueryStringParameters
    {
        private const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = MaxPageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
