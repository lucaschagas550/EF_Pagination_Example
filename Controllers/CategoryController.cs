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
    [Authorize(Roles = "Admin")]
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(INotifier notifier, IAspNetUser aspNetUser, ICategoryService categoryService) : base(notifier, aspNetUser) =>
            _categoryService = categoryService;

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Category>>> Get([FromQuery] CategoryPage pagination) =>
            CustomResponse(await _categoryService.Get(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> GetById(Guid id) =>
             CustomResponse(await _categoryService.GetById(id, CancellationToken.None).ConfigureAwait(false));

        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _categoryService.Create(category, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Put(Category category) =>
            CustomResponse(await _categoryService.Update(category, CancellationToken.None).ConfigureAwait(false));

        //receber o id, validar e remover, ajustar service
        [HttpDelete("{category}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Category category)
        {
            await _categoryService.Delete(category, CancellationToken.None).ConfigureAwait(false);
            return CustomResponse();
        }
    }
}
