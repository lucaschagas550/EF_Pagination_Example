using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    [Table("Products")]
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1.00, 99999.99, ErrorMessage = "The {0} must be between{1} e {2}")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        /* EF One-to-Many */
        public Supplier? Supplier { get; set; }

        /*  EF Many-to-Many */
        public List<CategoryProduct> CategoryProduct { get; set; }

        public Product()
        {
            CategoryProduct = new List<CategoryProduct>();
        }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryProduct = new List<CategoryProduct>();
        }

        public void SetCategoryProductId()
        {
            foreach (var item in CategoryProduct)
            {
                item.ProductId = Id;
            }
        }
    }
}
