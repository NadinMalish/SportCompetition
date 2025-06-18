using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Controllers;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;

namespace ControllerTests
{
    public class ApplicationStatusesControllerTests
    {
        DbContextOptions<Context> _options;
        Context _context;
        ApplicationStatusesController _applicationStatusesController;
        IFixture _fixture;

        public ApplicationStatusesControllerTests()
        {
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _context = new Context(_options);
            _fixture.Register(() => new StatusRepository(_context));

            _applicationStatusesController = _fixture.Build<ApplicationStatusesController>().OmitAutoProperties().Create();

        }

        //Создает и заносит ApplicationStatus в бд
        public ApplicationStatus GetStatus()
        {
            ApplicationStatus applicationStatus = _fixture.Build<ApplicationStatus>().Without(s => s.EventParticipants).Create();
            addToDb(applicationStatus);
            return applicationStatus;
        }

        private void addToDb(BaseEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllStatusesAsync_StatusesExists_ReturnsAllStatuses() 
        {
            //arrange
            List<ApplicationStatus> testStatuses = new List<ApplicationStatus>() { GetStatus(), GetStatus() };

            //act
            var actionResult = await _applicationStatusesController.GetAllStatusesAsync();
            
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedStatuses = Assert.IsType<List<ApplicationStatusResponse>>(okResult.Value);
            Assert.Equal(testStatuses.Select(s => s.Id), returnedStatuses.Select(s => s.Id));
        }

        [Fact]
        public async Task GetStatusByIdAsync_StatusIsExist_ReturnsStatus() 
        {
            //arrange
            ApplicationStatus status = GetStatus();

            //act
            var actionResult = await _applicationStatusesController.GetStatusByIdAsync(status.Id);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedStatus = Assert.IsType<ApplicationStatusResponse>(okResult.Value);
            Assert.Equal(status.Id, returnedStatus.Id);
        }

        [Theory, AutoData]
        public async Task GetStatusByIdAsync_StatusNotExist_ReturnsNotFound(int statusId)
        {
            //act
            var actionResult = await _applicationStatusesController.GetStatusByIdAsync(statusId);

            //assert
            actionResult.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Theory, AutoData]
        public async Task CreateStatusAsync_StatusIsValid_ShouldBeCreated(CreateOrEditApplicationStatusRequest request)
        {
            //act
            var actionResult = await _applicationStatusesController.CreateStatusAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<CreatedResult>();
            Assert.NotNull(_context.ApplicationStatuses.SingleOrDefault(s => s.Name == request.Name));    
        }

        [Fact]
        public async Task CreateStatusAsync_StatusWithThisNameAlreadyExists_ReturnsBadRequest()
        {
            //arrange
            ApplicationStatus status = GetStatus();
            CreateOrEditApplicationStatusRequest request = new CreateOrEditApplicationStatusRequest() { Name = status.Name };

            //act
            var actionResult = await _applicationStatusesController.CreateStatusAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Theory, AutoData]
        public async Task EditStatusAsync_StatusIsValid_ShouldBeEdited(CreateOrEditApplicationStatusRequest request) 
        {
            //arrange
            ApplicationStatus applicationStatus = GetStatus();

            //act
            var actionResult = await _applicationStatusesController.EditStatusAsync(applicationStatus.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
            Assert.Equal(_context.ApplicationStatuses.Single(s => s.Id == applicationStatus.Id).Name, request.Name);    
        }

        [Theory, AutoData]
        public async Task EditStatusAsync_StatusNotExists_ReturnsNotFound(int statusId, CreateOrEditApplicationStatusRequest request)
        {
            //act
            var actionResult = await _applicationStatusesController.EditStatusAsync(statusId, request);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task EditStatusAsync_StatusWithThisNameAlreadyExists_ReturnsBadRequest()
        {
            //arrange
            ApplicationStatus applicationStatus = GetStatus();
            CreateOrEditApplicationStatusRequest request = new CreateOrEditApplicationStatusRequest() { Name = applicationStatus.Name };

            //act
            var actionResult = await _applicationStatusesController.EditStatusAsync(applicationStatus.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteStatusAsync_StatusIsExist_ReturnsNoContent() 
        {
            //arrange
            ApplicationStatus status = GetStatus();

            //act
            var actionResult = await _applicationStatusesController.DeleteStatusAsync(status.Id);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
        }

        [Theory, AutoData]
        public async Task DeleteStatusAsync_StatusNotExist_ReturnsNotFound(int statusId) 
        {
            //act
            var actionResult = await _applicationStatusesController.DeleteStatusAsync(statusId);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }
    }
}
