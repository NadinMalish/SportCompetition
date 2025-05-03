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


namespace ControllerTests
{
    public class DocTypeControllerTests
    {
        private readonly DocTypeController _docTypeController;
        public DocTypeControllerTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _docTypeController = fixture.Build<DocTypeController>().OmitAutoProperties().Create();
        }

        //public DocType CreateDocType()
        //{
        //    DocType docType = _fixture.Build<DocType>().Without(x => x.Docs).Create();
        //    return docType;
        //}


        [Fact]
        public void GetSpisDocType_Valid_ifExist_NewRecord_inAllSpis()
        {
            //// Arrange
            //List<DocType> testDocType = new List<DocType>() { CreateDocType() };

            ////act
            //var actionResult = _docTypeController.GetSpisDocType();

            ////assert
            //Assert.IsType<OkObjectResult>(actionResult); 
            //Assert.NotNull(actionResult);
        }


    }
}
