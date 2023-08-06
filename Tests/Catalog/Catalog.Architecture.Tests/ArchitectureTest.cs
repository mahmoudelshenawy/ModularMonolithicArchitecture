using FluentAssertions;
using Module.Catalog;
using NetArchTest.Rules;
using Api;
using System.Reflection;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Controllers;
using Module.Catalog.Infrastructure.Persistence;

namespace Catalog.Architecture.Tests
{
    public class ArchitectureTest
    {
        private const string CatalogNameSpace = "Modules.Catalog.Module.Catalog";
        private const string CatalogCoreNameSpace = "Modules.Catalog.Module.Catalog.Core";
        private const string CatalogInfrastructureNameSpace = "Modules.Catalog.Module.Catalog.Infrastructure";
        private const string CatalogSharedNameSpace = "Modules.Catalog.Module.Catalog.Shared";
        private const string HostApiNameSpace = "Api";

        [Fact]
        public void Catalog_Should_Not_HaveDependencyOnSharedProject()
        {
            //Arrange
            var assembly = typeof(CatalogAssembly).Assembly;
            var otherProjects = new[]
            {
                CatalogSharedNameSpace
            };
            //Act
            TestResult testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void CatalogInfrastructure_Should_HaveDependencyOnCatalogCoreCore()
        {
            //Arrange
            var assembly = typeof(CatalogDbContext).Assembly;
            var otherProjects = new[]
            {
                CatalogCoreNameSpace
            };
            //Act
            TestResult testResult = Types.InAssembly(assembly).Should().HaveDependencyOnAll(otherProjects).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }


        [Fact]
        public void Controllers_Shoould_HaveDependencyOnMediatR()
        {
            var assembly = typeof(CategoryController).Assembly;

            var testResult = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR").GetResult();

            testResult.IsSuccessful.Should().BeTrue();

        }
        [Fact]
        public void Controllers_Shoould_Not_HaveDependencyOnDbContext()
        {
            var assembly = typeof(CategoryController).Assembly;

            var testResult = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .ShouldNot()
                .HaveDependencyOn("ICatalogDbContext").GetResult();

            testResult.IsSuccessful.Should().BeTrue();

        }

    }
}
