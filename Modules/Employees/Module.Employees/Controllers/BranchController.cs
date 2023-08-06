using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Employees.Core.Commands.Branches.CreateBranch;
using Module.Employees.Core.Commands.Branches.DeleteBranch;
using Module.Employees.Core.Commands.Branches.UpdateBranch;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Queries.Branches.GetAllAsync;
using Module.Employees.Core.Queries.Branches.GetByIdAsync;
using Shared.Core.Abstractions;
using Shared.Core.Common;

namespace Module.Employees.Controllers
{
    [ApiController]
    [Route("/api/admin/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class BranchController : ApiController
    {

        public BranchController(ISender sender) : base(sender)
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await Sender.Send(new GetAllBranchesAsyncQuery(pageNumber, pageSize)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Sender.Send(new GetBranchByIdAsyncQuery(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(BranchDto dto)
        {
            try
            {
              var result =  await Sender.Send(new CreateBranchAsyncCommand(dto.Name));
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(BranchDto branchDto)
        {
            try
            {
                return Ok(await Sender.Send(new UpdateBranchAsyncCommand(branchDto)));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Sender.Send(new DeleteBranchAsyncCommand(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
