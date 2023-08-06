using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Catalog.Core.Commands.Brands.CreateBrand;
using Module.Catalog.Core.Commands.Brands.DeleteBrand;
using Module.Catalog.Core.Commands.Brands.UpdateBrand;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Queries.Brands.GetAllAsync;
using Module.Catalog.Core.Queries.Brands.GetByIdAsync;
using Module.Catalog.Core.Queries.Users.GetUserDetails;
using Shared.Core.Common;
using Shared.Core.Interfaces;

namespace Module.Catalog.Controllers
{
    [ApiController]
    [Route("/api/catalog/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    internal class BrandController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;

        public BrandController(ISender sender, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllBrandsQuery()));
        }
         [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _sender.Send(new GetByIdAsyncQuery(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandDto brandDto)
        {
            return Ok(await _sender.Send(new CreateBrandCommand(brandDto)));
        }

         [HttpPut]
        public async Task<IActionResult> Update(BrandDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            return Ok(await _sender.Send(new UpdateBrandCommand(brandDto)));
        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _sender.Send(new DeleteBrandCommand(id)));
        }

        [HttpGet("get-user-details-test")]
        public async Task<IActionResult> GetUserDetailsTest()
        {
            return Ok(await _sender.Send(new GetUserDetailsQuery(_currentUserService.userId)));
        }
    }
}
