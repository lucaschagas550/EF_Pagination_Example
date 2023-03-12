using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers
{
    [Route("[controller]")]
    public class CategoryController : MainController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(INotifier notifier, ICategoryServices categoryServices) : base(notifier)=>
            _categoryServices = categoryServices;

        [HttpGet()]
        public async Task<ActionResult<Page<Category>>> Get([FromQuery] CategoryPage pagination) =>
            CustomResponse(await _categoryServices.Get(pagination).ConfigureAwait(false));

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id) =>
            CustomResponse(await _categoryServices.GetById(id).ConfigureAwait(false));

        [HttpPost()]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _categoryServices.Create(category).ConfigureAwait(false));
        }

        [HttpPut()]
        public async Task<ActionResult<Category>> Put(Category category) =>
            CustomResponse(await _categoryServices.Create(category).ConfigureAwait(false));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            await _categoryServices.Delete(id).ConfigureAwait(false);
            return CustomResponse();
        }
    }
}
