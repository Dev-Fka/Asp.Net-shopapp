using shopapp.entity;

public class PageInfo
{
    public int totalItems { get; set; }
    public int ıtemsPerPage { get; set; }
    public int currentPage { get; set; }
    public string? currentCategory { get; set; }

    public int totalPages()
    {
        return (int)Math.Ceiling(((decimal)totalItems / ıtemsPerPage));
    }
}

public class ProductListViewModel
{
    public PageInfo? pageInfo { get; set; }
    public List<Product>? Products { get; set; }
}