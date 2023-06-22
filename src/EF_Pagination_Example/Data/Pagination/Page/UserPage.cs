using EF_Pagination_Example.Data.Pagination.Base;

namespace EF_Pagination_Example.Data.Pagination.Page
{
    public class UserPage : Pageable
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}