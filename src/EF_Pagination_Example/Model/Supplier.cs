using EF_Pagination_Example.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    [Table("Suppliers")]
    public class Supplier : Entity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Inform the document.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The {0} must have a maximum of {1} characters and a minimum of {2}.")]
        public string Document { get; set; } = string.Empty;

        [Required]
        public ETypeSupplier TypeSupplier { get; set; }
        public ESupplierStatus Active { get; set; }

        /* EF One-to-Many */
        public List<Product> Products { get; set; }

        public Supplier() 
        { 
            Products = new List<Product>();
        }

        public Supplier(string name, string document, ETypeSupplier typeSupplier, ESupplierStatus active)
        {
            Name = name;
            Document = document;
            TypeSupplier = typeSupplier;
            Active = active;
            Products = new List<Product>();
        }
    }
}
