using Bogus;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Business.Services;
using EF_Pagination_Example.Controllers;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.Test.Helpers;
using Moq.AutoMock;

namespace EF_Pagination_Example.Test.ProductTest
{
    public class ProductControllerTestFixture : IDisposable
    {
        public List<Product> products = new List<Product>();

        public ProductController productController;
        public ProductService productService;
        public Notifier notifier;
        public AutoMocker Mocker;

        public ProductControllerTestFixture()
        {
            GetProducts();

            GetProductController();
        }

        private ProductController GetProductController()
        {
            Mocker = new AutoMocker();

            productService = Mocker.CreateInstance<ProductService>();
            notifier = Mocker.CreateInstance<Notifier>();
            productController = new ProductController(notifier, productService);

            InitializeController.Context(productController);
            return productController;
        }

        private void GetProducts()
        {
            products.AddRange(GenerateProducts(50).ToList());
        }

        private IEnumerable<Product> GenerateProducts(int quantity)
        {
            var product = new Faker<Product>("en")
                .CustomInstantiator(p => new Product(
                    p.Commerce.Product(),
                    p.Commerce.ProductDescription(),
                    Convert.ToDecimal(p.Commerce.Price())
                   ));

            return product.Generate(quantity);
        }

        public void Dispose()
        {
        }
    }
}
