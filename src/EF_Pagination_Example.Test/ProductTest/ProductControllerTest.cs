using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Test.Helpers;
using Moq;
using Newtonsoft.Json;

namespace EF_Pagination_Example.Test.ProductTest
{
    public class ProductControllerTest : IClassFixture<ProductControllerTestFixture>
    {
        private readonly ProductControllerTestFixture _fixture;

        public ProductControllerTest(ProductControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Test1()
        {
            //Arrange
            var expectedResult = JsonConvert.SerializeObject(new ResponseSuccess(_fixture.products.FirstOrDefault()));

            _fixture.Mocker.GetMock<IProductRepository>().Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.products.FirstOrDefault());

            //Action
            var actionResult = Response.GetResponse(await _fixture.productController.GetById(Guid.NewGuid()).ConfigureAwait(false));

            //Result
            Assert.Equal(expectedResult, actionResult);
            _fixture.Mocker.GetMock<IProductRepository>().Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
