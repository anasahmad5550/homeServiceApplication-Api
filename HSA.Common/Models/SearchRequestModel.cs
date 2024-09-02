namespace HSA.Common.Models
{
    public class SearchRequestModel
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string? searchText { get; set; }
    }
}
