namespace CatalogDb.API.Pagination.Filters
{
    public class BaseFilter<T> where T : class
    {
        public int PageNumber { get; set; } = 1;
        private const int MaxPageSize = 10;

        private int _pageSize = MaxPageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public virtual IQueryable<T> HandleFilter(IQueryable<T> filter)
        {
            return filter;
        }
    }
}
