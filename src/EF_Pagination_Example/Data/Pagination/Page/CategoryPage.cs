using Azure;
using EF_Pagination_Example.Data.Pagination.Base;

namespace EF_Pagination_Example.Data.Pagination.Page
{
    public class CategoryPage : Pageable
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
