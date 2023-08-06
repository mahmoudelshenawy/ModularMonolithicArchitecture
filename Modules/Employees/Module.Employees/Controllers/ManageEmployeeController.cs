using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Employees.Core.Commands.Employees.CreateEmployee;
using Module.Employees.Core.Commands.Employees.UpdateEmployee;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Queries.Employees.GetAllAsync;
using Module.Employees.Core.Queries.Employees.GetByIdAsync;
using Shared.Core.Abstractions;
using Shared.Core.Common;
using Shared.Models.Models;

namespace Module.Employees.Controllers
{
    [ApiController]
    [Route("/api/admin/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class ManageEmployeeController : ApiController
    {
        public ManageEmployeeController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber , [FromQuery] int pageSize)
        {
            var result = await Sender.Send(new GetAllEmployeesAsyncQuery(pageNumber , pageSize));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result);
        }
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                var result = await Sender.Send(new GetEmployeeByIdAsyncQuery(Id));
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            try
            {
                Result<CreateEmployeeDto> result = await Sender.Send(new CreateEmployeeCommand(dto));
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeDto dto)
        {
            try
            {
                Result<UpdateEmployeeDto> result = await Sender.Send(new UpdateEmployeeAsyncCommand(dto));
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
