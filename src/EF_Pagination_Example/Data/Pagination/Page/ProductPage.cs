using EF_Pagination_Example.Data.Pagination.Base;

namespace EF_Pagination_Example.Data.Pagination.Page
{
    public class ProductPage : Pageable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal LowestPrice { get; set; }
        public decimal HigherPrice { get; set; }
    }
}
