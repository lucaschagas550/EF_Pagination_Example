using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    public class CategoryProduct : Entity
    {
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Category Category;

        public Product Product;

        public CategoryProduct() { }

        public CategoryProduct(Guid categoryId, Guid productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }
    }
}
