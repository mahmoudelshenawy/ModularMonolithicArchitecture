using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Catalog.Core.Commands.Categories.CreateCategory;
using Module.Catalog.Core.Commands.Categories.DeleteCategory;
using Module.Catalog.Core.Commands.Categories.UpdateCategory;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Queries.Categories.GetAllCategoriesAsync;
using Module.Catalog.Core.Queries.Categories.GetCategoryByIdAsync;
using Shared.Core.Common;
using Shared.Core.Interfaces;

namespace Module.Catalog.Controllers
{
    [ApiController]
    [Route("/api/catalog/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        public CategoryController(ISender sender, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllCategoriesAsyncQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _sender.Send(new GetCategoryByIdAsyncQuery(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            return Ok(await _sender.Send(new CreateCategoryCommand(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto dto)
        {
            return Ok(await _sender.Send(new UpdateCategoryCommand(dto)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _sender.Send(new DeleteCategoryCommand(id)));
        }
    }
}
