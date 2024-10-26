namespace ApiUdemy.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSizze { get; private set; }
    public int TotalCount { get; private set; }

    public bool HasPreciousss => CurrentPage > 1;

    public bool HasNex => CurrentPage < TotalPages;


}
