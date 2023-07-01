using EF_Pagination_Example.Business.Interfaces;
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
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(INotifier notifier, ICategoryService categoryService) : base(notifier) =>
            _categoryService = categoryService;

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Category>>> Get([FromQuery] CategoryPage pagination) =>
            CustomResponse(await _categoryService.GetAsync(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> GetById(Guid id) =>
             CustomResponse(await _categoryService.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false));

        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _categoryService.CreateAsync(category, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Put(Category category)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _categoryService.UpdateAsync(category, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id) =>
            CustomResponse(await _categoryService.DeleteAsync(id, CancellationToken.None).ConfigureAwait(false));
    }
}
