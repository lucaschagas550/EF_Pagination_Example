using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    public class CategoryProduct : Entity
    {
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Category Category { get; set; } = new Category();
        public virtual Product Product { get; set; } = new Product();

        public CategoryProduct() { }

        public CategoryProduct(Guid categoryId, Guid productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }
    }
}
