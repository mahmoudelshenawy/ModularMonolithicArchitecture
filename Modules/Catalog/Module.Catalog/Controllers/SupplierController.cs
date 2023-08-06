using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Catalog.Core.Commands.Suppliers.CreateSupplier;
using Module.Catalog.Core.Commands.Suppliers.DeleteSupplier;
using Module.Catalog.Core.Commands.Suppliers.UpdateSupplier;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Queries.Suppliers.GetAllAsync;
using Module.Catalog.Core.Queries.Suppliers.GetByIdAsync;
using Shared.Core.Common;

namespace Module.Catalog.Controllers
{
    [ApiController]
    [Route("/api/catalog/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class SupplierController : ControllerBase
    {
        private readonly ISender _sender;

        public SupplierController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllProductsAsyncQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _sender.Send(new GetProductByIdAsyncQuery(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(SupplierDto dto)
        {
            return Ok(await _sender.Send(new CreateProductAsyncCommand(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SupplierDto dto)
        {
            return Ok(await _sender.Send(new UpdateProductAsyncCommand(dto)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _sender.Send(new DeleteProductAsyncCommand(id)));
        }
    }
}
