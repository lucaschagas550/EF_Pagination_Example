using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF_Pagination_Example.Model
{
    [Table("Categories")]
    public class Category : Entity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        /*  EF Many-to-Many */
        public List<CategoryProduct> CategoryProduct { get; set; }

        public Category()
        {
            CategoryProduct = new List<CategoryProduct>();
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
            CategoryProduct = new List<CategoryProduct>();
        }

        public void SetCategoryId() =>
            CategoryProduct.Select(c => c.CategoryId = Id);
    }
}
