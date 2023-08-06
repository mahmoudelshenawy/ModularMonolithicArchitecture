using Microsoft.EntityFrameworkCore;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Commands.Departments.CreateDepartment;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Moq;
using Shared.Models.Models;

namespace Employees.Tests.Commands
{
    public class CreateDepartmentAsyncCommandHandlerTests
    {
        private readonly Mock<IEmployeeDbContext> _contextMock;
        public CreateDepartmentAsyncCommandHandlerTests()
        {
            _contextMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenDataIsProvidedSuccesfully()
        {
            //Arrange
            var command = new CreateDepartmentAsyncCommand("Back-End Development", 1);
            var handler = new CreateDepartmentAsyncCommandHandler(_contextMock.Object);
            var dbset = new Mock<DbSet<Department>>();
            _contextMock.Setup(mc => mc.Departments).Returns(dbset.Object);
            //Act
            Result<DepartmentDto> result = await handler.Handle(command, default);
            //Assert
            _contextMock.Verify(
                 x => x.Departments.AddAsync(It.Is<Department>(m => m.Id == result.Value.Id), default )
                , Times.Once
                );

        }
        [Fact]
        public async Task Handle_Should_NotCallSaveChangesAsyncWhenDataIsNotProvided()
        {
            //Arrange
            var command = new CreateDepartmentAsyncCommand("", 0);
            var handler = new CreateDepartmentAsyncCommandHandler(_contextMock.Object);
            var test = new List<Department> { Department.Create("test", 1), Department.Create("test2" , 1) };
            var dbset = new Mock<DbSet<Department>>(test);
           
            _contextMock.Setup(mc => mc.Departments).Returns(dbset.Object);
            //Act
            await handler.Handle(command, default);
            //Assert
            _contextMock.Verify(
                 x => x.SaveChangesAsync(default)
                , Times.Never
                );
        }
    }
}
