using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
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
using Moq;
using Services.Repositories.Abstractions;
using YamlDotNet.Core;
using Microsoft.AspNetCore.Http;


namespace ControllerTests
{
    public class DocTypeControllerTests
    {
        DbContextOptions<Context> _options;
        Context _context;
        DocTypeController _doctypeController;
        IFixture _fixture;

        public DocTypeControllerTests()
        {

            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _context = new Context(_options);
            _fixture.Register(() => new EFRepository<DocType>(_context));

            _doctypeController = _fixture.Build<DocTypeController>().OmitAutoProperties().Create();
        }

        public DocType GetItem()
        {
            DocType item = _fixture.Build<DocType>().Without(s => s.Docs).Create();
            addToDb(item);
            return item;
        }

        private void addToDb(BaseEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }


        [Fact]
        public async Task GetSpisDocType_Valid_inAllSpis()
        {
            //// Act
            //var result = await _doctypeController.GetSpisDocType();

            //// Assert
            //var actionResult = Assert.IsType<ActionResult<List<DocTypeShortResponse>>>(result);
            //Assert.IsType<OkObjectResult>(actionResult.Result);


            //Arrange
            List<DocType> testItem = new List<DocType>() { GetItem(), GetItem() };

            //Act
            var result = await _doctypeController.GetSpisDocType();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actionResult = Assert.IsType<List<DocTypeShortResponse>>(okResult.Value);
            Assert.Equal(testItem.Select(s => s.Id), actionResult.Select(s => s.Id));

        }


    }
}
