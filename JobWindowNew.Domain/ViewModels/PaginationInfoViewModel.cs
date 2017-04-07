namespace JobWindowNew.Domain.ViewModels
{
    public class PaginationInfoViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string SortDirection { get; set; }
        public string SortField { get; set; }
    }
}
