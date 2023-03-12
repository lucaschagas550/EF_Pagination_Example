using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices) =>
            _categoryServices = categoryServices;

        [HttpGet()]
        public async Task<ActionResult<Page<Category>>> Get([FromQuery] CategoryPage pagination) =>
            Ok(await _categoryServices.Get(pagination).ConfigureAwait(false));

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id) =>
            Ok(await _categoryServices.GetById(id).ConfigureAwait(false));

        [HttpPost()]
        public async Task<ActionResult<Category>> Post(Category category) =>
            Created($"{HttpContext.Request.PathBase}/{HttpContext.Request.Path}", await _categoryServices.Create(category).ConfigureAwait(false));

        [HttpPut()]
        public async Task<ActionResult<Category>> Put(Category category) =>
            Ok(await _categoryServices.Create(category).ConfigureAwait(false));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            await _categoryServices.Delete(id).ConfigureAwait(false);
            return Ok();
        }
    }
}
