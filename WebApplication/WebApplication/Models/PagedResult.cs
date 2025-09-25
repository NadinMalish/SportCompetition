namespace WebApplication.Models
{
    public class PagedResult
    {
        public List<EventInfoResponse> Events { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
