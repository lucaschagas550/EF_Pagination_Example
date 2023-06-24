using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Services;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin, User")]
    public class ProductController : MainController
    {
        private readonly IProductService _productService;

        public ProductController(INotifier notifier, IAspNetUser aspNetUser, IProductService productService) : base(notifier, aspNetUser) =>
            _productService = productService;

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Product>>> Get([FromQuery] ProductPage pagination) =>
            CustomResponse(await _productService.GetAsync(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetById(Guid id) =>
             CustomResponse(await _productService.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false));

        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Post(Product product)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _productService.CreateAsync(product, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Put(Product product)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _productService.UpdateAsync(product, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id) =>
            CustomResponse(await _productService.DeleteAsync(id, CancellationToken.None).ConfigureAwait(false));
    }
}
