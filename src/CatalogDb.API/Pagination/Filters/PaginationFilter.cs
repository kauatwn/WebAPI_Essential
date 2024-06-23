namespace CatalogDb.API.Pagination.Filters
{
    public class PaginationFilter<T> where T : class
    {
        private const int MaxPageSize = 10;
        private int _pageSize = MaxPageSize;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value < 1 ? 1 : value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public virtual IQueryable<T> HandleFilter(IQueryable<T> source)
        {
            return source.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
    }
}
