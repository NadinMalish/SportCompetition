using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Controllers;
using WebApplication.Models;

namespace ControllerTests
{
    public class RolesControllerTests
    {
        DbContextOptions<Context> _options;
        Context _context;
        RolesController _rolesController;
        IFixture _fixture;

        public RolesControllerTests()
        {
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _context = new Context(_options);
            _fixture.Register(() => new RoleRepository(_context));

            _rolesController = _fixture.Build<RolesController>().OmitAutoProperties().Create();

        }

        //Создает и заносит Role в бд
        public Role GetRole()
        {
            Role role = _fixture.Build<Role>().Without(s => s.EventParticipants).Create();
            addToDb(role);
            return role;
        }

        private void addToDb(BaseEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllRolesAsync_RolesExists_ReturnsAllRoles()
        {
            //arrange
            List<Role> testRoles = new List<Role>() { GetRole(), GetRole() };

            //act
            var actionResult = await _rolesController.GetAllRolesAsync();

            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedRole = Assert.IsType<List<RoleResponse>>(okResult.Value);
            Assert.Equal(testRoles.Select(s => s.Id), returnedRole.Select(s => s.Id));
        }

        [Fact]
        public async Task GetRoleByIdAsync_RoleIsExist_ReturnsRole()
        {
            //arrange
            Role role = GetRole();

            //act
            var actionResult = await _rolesController.GetRoleByIdAsync(role.Id);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedRole = Assert.IsType<RoleResponse>(okResult.Value);
            Assert.Equal(role.Id, returnedRole.Id);
        }

        [Theory, AutoData]
        public async Task GetRoleByIdAsync_RoleNotExist_ReturnsNotFound(int roleId)
        {
            //act
            var actionResult = await _rolesController.GetRoleByIdAsync(roleId);

            //assert
            actionResult.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Theory, AutoData]
        public async Task CreateRoleAsync_RoleIsValid_ShouldBeCreated(CreateOrEditRoleRequest request)
        {
            //act
            var actionResult = await _rolesController.CreateRoleAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<CreatedResult>();
            Assert.NotNull(_context.Roles.SingleOrDefault(s => s.Name == request.Name));
        }

        [Fact]
        public async Task CreateRoleAsync_RoleWithThisNameAlreadyExists_ReturnsBadRequest()
        {
            //arrange
            Role role = GetRole();
            CreateOrEditRoleRequest request = new CreateOrEditRoleRequest() { Name = role.Name };

            //act
            var actionResult = await _rolesController.CreateRoleAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Theory, AutoData]
        public async Task EditRoleAsync_RoleIsValid_ShouldBeEdited(CreateOrEditRoleRequest request)
        {
            //arrange
            Role role = GetRole();

            //act
            var actionResult = await _rolesController.EditRoleAsync(role.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
            Assert.Equal(_context.Roles.Single(s => s.Id == role.Id).Name, request.Name);
        }

        [Theory, AutoData]
        public async Task EditRoleAsync_RoleNotExists_ReturnsNotFound(int roleId, CreateOrEditRoleRequest request)
        {
            //act
            var actionResult = await _rolesController.EditRoleAsync(roleId, request);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task EditRoleAsync_RoleWithThisNameAlreadyExists_ReturnsBadRequest()
        {
            //arrange
            Role role = GetRole();
            CreateOrEditRoleRequest request = new CreateOrEditRoleRequest() { Name = role.Name };

            //act
            var actionResult = await _rolesController.EditRoleAsync(role.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteRoleAsync_RoleIsExist_ReturnsNoContent()
        {
            //arrange
            Role role = GetRole();

            //act
            var actionResult = await _rolesController.DeleteRoleAsync(role.Id);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
        }

        [Theory, AutoData]
        public async Task DeleteRoleAsync_RoleNotExist_ReturnsNotFound(int roleId)
        {
            //act
            var actionResult = await _rolesController.DeleteRoleAsync(roleId);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }
    }
}
