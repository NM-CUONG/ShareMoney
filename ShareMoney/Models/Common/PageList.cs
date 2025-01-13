using Microsoft.Identity.Client;

namespace Web.Models.Common
{
    public class PageList
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; } = "GetData";
    }
}
