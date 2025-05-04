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
        private readonly DocTypeController _doctypeController;
        IFixture _fixture;

        public DocTypeControllerTests()
        {
            var _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _doctypeController = _fixture.Build<DocTypeController>().OmitAutoProperties().Create();
        }

        [Fact]
        public async Task GetSpisDocType_Valid_inAllSpis()
        {
            // Act
            var result = await _doctypeController.GetSpisDocType();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<DocTypeShortResponse>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }


    }
}
