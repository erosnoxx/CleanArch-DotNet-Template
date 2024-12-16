using Application.Attributes;

namespace Application.Models.Pagination
{
    public class PaginationInput
    {
        [MinValue(1)]
        public int PageNumber { get; set; }

        [MinValue(1)]
        public int PageSize { get; set; }

    }
}
