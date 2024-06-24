namespace GatewayOcelot.API.Settings.PaginationSettings;

public class PageList<TEntity>
    where TEntity : class
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<TEntity> Data { get; set; }

    public PageList() { }

    public PageList(List<TEntity> items, long count, PageParameters pageParameters)
    {
        Data = items;
        TotalCount = count;
        PageSize = pageParameters.PageSize;
        CurrentPage = pageParameters.PageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageParameters.PageSize);
    }
}
