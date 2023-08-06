using AutoMapper;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Module.Users.Shared.UserApiInterfaces;
using Shared.Core.Common;
using Shared.Core.Common.Messaging;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Employees.CreateEmployee
{
    public record CreateEmployeeCommand(CreateEmployeeDto EmployeeDto) : ICommand<CreateEmployeeDto>;

    internal sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserPublicApi _userApi;
        public CreateEmployeeCommandHandler(IEmployeeDbContext context, IMapper mapper, IUserPublicApi userApi)
        {
            _context = context;
            _mapper = mapper;
            _userApi = userApi;
        }

        public async Task<Result<CreateEmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.EmployeeDto.Email) &&
                await _userApi.EmailIsUnique(request.EmployeeDto.Email) == false)
            {
                return Result.Failure<CreateEmployeeDto>(new Error("Employee.Email", "email is already taken"));
            }

            request.EmployeeDto.Id = Guid.NewGuid();
            Employee? employee = Employee.Create(request.EmployeeDto);
            try
            {

                //Create user
                var userResult = await _userApi.RegisterNewUser(request.EmployeeDto.Email,
                    request.EmployeeDto.Name, request.EmployeeDto.Phone, request.EmployeeDto.Password,
                    new string[] { RolesNameConstants.EmployeeSales });
                if (userResult.IsFailure)
                {
                    return Result.Failure<CreateEmployeeDto>(new Error("Employee.User.Register", "unable to register user in the system"));
                }
                employee = Employee.AddUserIdToEmployee(employee, userResult.Value);
                await _context.Employees.AddAsync(employee);
                var result = await _context.SaveChangesAsync(cancellationToken);
                if (result == 0)
                {
                    return Result.Failure<CreateEmployeeDto>(new Error("Employee.Create", "something went wrong"));
                }
                request.EmployeeDto.UserId = userResult.Value;
                return Result.Success(request.EmployeeDto);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
