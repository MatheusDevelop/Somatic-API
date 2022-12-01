namespace UI.Aws.Utils
{
    public static class QueryUtils
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int? page, int? itemsPerPage)
        {
            int pageAsInt = page ?? 0;
            int itemsPerPageAsInt = itemsPerPage ?? 0;
            if (page == null && itemsPerPage == null)
                return query.Skip(0).Take(25);
            if (page is null || page == 0)
                pageAsInt = 1;
            if (itemsPerPage is null || itemsPerPage == 0)
                itemsPerPageAsInt = 25;
            if (page > 0 && itemsPerPage > 0 && page is not null && itemsPerPage is not null)
            {
                pageAsInt = (int)page;
                itemsPerPageAsInt = (int)itemsPerPage;
            }
            int skip = (pageAsInt - 1) * itemsPerPageAsInt;
            return query.Skip(skip).Take(itemsPerPageAsInt);
        }
    }
}
