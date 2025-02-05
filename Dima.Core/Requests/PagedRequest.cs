namespace Dima.Core.Requests;
public class PagedRequest : Request
{
    public int PagedNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}
