using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Model.Enum;

namespace EF_Pagination_Example.Data.Pagination.Page
{
    public class SupplierPage : Pageable
    {
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public ETypeSupplier TypeSupplier { get; set; }
        public ESupplierStatus Active { get; set; }
    }
}
