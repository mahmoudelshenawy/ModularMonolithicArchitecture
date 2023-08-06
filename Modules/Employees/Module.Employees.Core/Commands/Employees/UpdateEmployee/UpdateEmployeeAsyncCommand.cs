using AutoMapper;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Module.Users.Shared.UserApiInterfaces;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Employees.UpdateEmployee;

public sealed record UpdateEmployeeAsyncCommand(UpdateEmployeeDto EmployeeDto) : ICommand<UpdateEmployeeDto>;


internal sealed class UpdateEmployeeAsyncCommandHandler : ICommandHandler<UpdateEmployeeAsyncCommand, UpdateEmployeeDto>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserPublicApi _userApi;

    public UpdateEmployeeAsyncCommandHandler(IEmployeeDbContext context, IMapper mapper, IUserPublicApi userApi)
    {
        _context = context;
        _mapper = mapper;
        _userApi = userApi;
    }

    public async Task<Result<UpdateEmployeeDto>> Handle(UpdateEmployeeAsyncCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(request.EmployeeDto.Id);
        if (employee is null) throw new EntityNotFound("Employee of id" + request.EmployeeDto.Id);

        //update user data
        var employeeCurrentEmail = await _userApi.GetUserEmailById(employee.PersonalInformation.UserId);
        if(employeeCurrentEmail == request.EmployeeDto.Email || string.IsNullOrEmpty(request.EmployeeDto.Password))
        {
           
        }
        //update used credentials
        var userUpdatedResult = await _userApi.UpdateUserCredentials(request.EmployeeDto.UserId, request.EmployeeDto.Email,
            request.EmployeeDto.Password, request.EmployeeDto.Phone);
        //update employee record
        Employee updatedEmployee = Employee.Update(employee, request.EmployeeDto);

        _context.Employees.Update(updatedEmployee);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(request.EmployeeDto);
        //optional update user roles
    }
}