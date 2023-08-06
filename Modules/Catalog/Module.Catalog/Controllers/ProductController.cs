using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Catalog.Core.Commands.Products.CreateSupplier;
using Module.Catalog.Core.Commands.Products.DeleteProduct;
using Module.Catalog.Core.Commands.Products.Updateproduct;
using Module.Catalog.Core.Commands.Products.UpdateStock;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Queries.Products.GetAllAsync;
using Module.Catalog.Core.Queries.Products.GetByIdAsync;
using Shared.Core.Common;
using Shared.Core.Interfaces;

namespace Module.Catalog.Controllers
{
    [ApiController]
    [Route("/api/catalog/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;

        public ProductController(ISender sender, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _sender.Send(new GetAllProductsAsyncQuery(pageNumber, pageSize)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _sender.Send(new GetProductByIdAsyncQuery(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            return Ok(await _sender.Send(new CreateProductAsyncCommand(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto dto)
        {
            return Ok(await _sender.Send(new UpdateProductAsyncCommand(dto)));
        }
        [HttpPut("updateProductStock")]
        public async Task<IActionResult> updateProductStock(ProductStockDto productStockDto)
        {
            return Ok(await _sender.Send(new UpdateProductStockCommand(productStockDto)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _sender.Send(new DeleteProductAsyncCommand(id)));
        }
    }
}
