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

        public Category() { }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
