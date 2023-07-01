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
    public class SupplierController : MainController
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(INotifier notifier, ISupplierServices supplierServices) : base(notifier) =>
            _supplierServices = supplierServices;

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<Supplier>>> Get([FromQuery] SupplierPage pagination) =>
            CustomResponse(await _supplierServices.GetAsync(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Supplier>> GetById(Guid id) =>
             CustomResponse(await _supplierServices.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false));

        [HttpPost()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _supplierServices.CreateAsync(supplier, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Supplier>> Put(Supplier supplier)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _supplierServices.UpdateAsync(supplier, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id) =>
            CustomResponse(await _supplierServices.DeleteAsync(id, CancellationToken.None).ConfigureAwait(false));
    }
}
