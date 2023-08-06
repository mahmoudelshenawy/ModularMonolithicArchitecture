using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Commands.Departments.CreateDepartment;
using Module.Employees.Core.Commands.Departments.DeleteDepartment;
using Module.Employees.Core.Commands.Departments.UpdateDepartment;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Queries.Departments.GetAllAsync;
using Module.Employees.Core.Queries.Departments.GetByIdAsync;
using Shared.Core.Abstractions;
using Shared.Core.Common;

namespace Module.Employees.Controllers
{
    [ApiController]
    [Route("/api/admin/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class DepartmentController : ApiController
    {
        

        public DepartmentController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Sender.Send(new GetAllDepartmentsAsyncQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Sender.Send(new GetDepartmentByIdAsyncQuery(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            try
            {
                var result = await Sender.Send(new CreateDepartmentAsyncCommand(dto.Name, dto.BranchId));

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
        public async Task<IActionResult> Update(DepartmentDto dto)
        {
            try
            {
                return Ok(await Sender.Send(new UpdateDepartmentAsyncCommand(dto)));
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
                return Ok(await Sender.Send(new DeleteDepartmentAsyncCommand(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
