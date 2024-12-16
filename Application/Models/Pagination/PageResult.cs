namespace Application.Models.Dtos.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int ItensPageCount { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
