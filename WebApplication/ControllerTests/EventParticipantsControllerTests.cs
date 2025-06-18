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
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;

namespace ControllerTests
{
    public class EventParticipantsControllerTests
    {
        DbContextOptions<Context> _options;
        Context _context;
        EventParticipantsController _eventParticipantsController;
        IFixture _fixture;
        public EventParticipantsControllerTests()
        {
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _context = new Context(_options);
            _fixture.Register(() => new EventParticipantRepository(_context));
            _fixture.Register(() => new StatusRepository(_context));

            _eventParticipantsController = _fixture.Build<EventParticipantsController>().OmitAutoProperties().Create();
            _fixture.Customize<ApplicationStatus>(b => b.Without(s => s.EventParticipants));
        }

        //Создает и заносит EventParticipant в бд
        public EventParticipant GetEventParticipant()
        {
            EventParticipant eventParticipant = _fixture.Build<EventParticipant>()
                                                        .Create();
            addToDb(eventParticipant);
            return eventParticipant;
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
        public async Task GetAllEventParticipantsAsync_EntitiesExists_ReturnsAllEntities()
        {
            //arrange
            List<EventParticipant> testRoles = new List<EventParticipant>() { GetEventParticipant(), GetEventParticipant() };

            //act
            var actionResult = await _eventParticipantsController.GetAllEventParticipantsAsync();

            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedStatuses = Assert.IsType<List<EventParticipantResponse>>(okResult.Value);
            Assert.Equal(testRoles.Select(s => s.Id), returnedStatuses.Select(s => s.Id));
        }

        [Fact]
        public async Task GetEventParticipantByIdAsync_EntityIsExist_ReturnsEntity()
        {
            //arrange
            EventParticipant eventParticipant = GetEventParticipant();

            //act
            var actionResult = await _eventParticipantsController.GetEventParticipantByIdAsync(eventParticipant.Id);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedEntity = Assert.IsType<EventParticipantResponse>(okResult.Value);
            Assert.Equal(eventParticipant.Id, returnedEntity.Id);
        }

        [Theory, AutoData]
        public async Task GetEventParticipantByIdAsync_EntityNotExist_ReturnsNotFound(int EventParticipantId)
        {
            //act
            var actionResult = await _eventParticipantsController.GetEventParticipantByIdAsync(EventParticipantId);

            //assert
            actionResult.Result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task CreateEventParticipantAsync_EntityIsValid_ShouldBeCreated()
        {
            //arrange
            ApplicationStatus status = GetStatus();
            CreateEventParticipantRequest request = new CreateEventParticipantRequest() { StatusId = status.Id };

            //act
            var actionResult = await _eventParticipantsController.CreateEventParticipantAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<CreatedResult>();
            Assert.NotNull(_context.EventParticipants.Single());
        }

        [Theory, AutoData]
        public async Task CreateEventParticipantAsync_RoleNotExists_ReturnsBadRequest(int roleId)
        {
            //arrange
            ApplicationStatus status = GetStatus();
            CreateEventParticipantRequest request = new CreateEventParticipantRequest() { StatusId = status.Id, RoleId = roleId };

            //act
            var actionResult = await _eventParticipantsController.CreateEventParticipantAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Theory, AutoData]
        public async Task CreateEventParticipantAsync_StatusNotExists_ReturnsBadRequest(int statusId)
        {
            //arrange
            CreateEventParticipantRequest request = new CreateEventParticipantRequest() { StatusId = statusId };

            //act
            var actionResult = await _eventParticipantsController.CreateEventParticipantAsync(request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Theory, AutoData]
        public async Task EditEventParticipantAsync_EntityIsValid_ShouldBeEdited(EditEventParticipantRequest request)
        {
            //arrange
            EventParticipant eventParticipant = GetEventParticipant();
            ApplicationStatus status = GetStatus();
            request.StatusId = status.Id;
          
            //act
            var actionResult = await _eventParticipantsController.EditEventParticipantAsync(eventParticipant.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
            Assert.Equal(_context.EventParticipants.Single(s => s.Id == eventParticipant.Id).ApplicationStatusId, request.StatusId);
        }

        [Theory, AutoData]
        public async Task EditEventParticipantAsync_EntityNotExists_ReturnsNotFound(int id, EditEventParticipantRequest request)
        {
            //act
            var actionResult = await _eventParticipantsController.EditEventParticipantAsync(id, request);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }

        [Theory, AutoData]
        public async Task EditEventParticipantAsync_StatusNotExists_ReturnsBadRequest(EditEventParticipantRequest request)
        {
            //arrange
            EventParticipant eventParticipant = GetEventParticipant();

            //act
            var actionResult = await _eventParticipantsController.EditEventParticipantAsync(eventParticipant.Id, request);

            //assert
            actionResult.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteEventParticipantAsync_EntityIsExists_ShouldBeSetFieldDeletedInTrue()
        {
            //arrange
            EventParticipant eventParticipant = GetEventParticipant();

            //act
            var actionResult = await _eventParticipantsController.DeleteEventParticipantAsync(eventParticipant.Id);

            //assert
            actionResult.Should().BeAssignableTo<NoContentResult>();
            Assert.True(_context.EventParticipants.Single(s => s.Id == eventParticipant.Id).IsDeleted);
        }

        [Theory, AutoData]
        public async Task DeleteEventParticipantAsync_EntityNotExists_ReturnsNotFound(int id)
        {
            //act
            var actionResult = await _eventParticipantsController.DeleteEventParticipantAsync(id);

            //assert
            actionResult.Should().BeAssignableTo<NotFoundResult>();
        }
    }
}
