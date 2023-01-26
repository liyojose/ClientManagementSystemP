using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace ClientManagementSystem.Test
{
    using AutoMapper;
    using ClientManagementSystem.Api.Controllers;
    using ClientManagementSystem.Common.Dto;
    using ClientManagementSystem.Common.Services;
    using ClientManagementSystem.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _mockClientService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClientController _controller;

        public ClientControllerTests()
        {
            _mockClientService = new Mock<IClientService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ClientController(_mockClientService.Object, _mockMapper.Object);
        }

        [Fact]
        public async void Post_Returns201Created()
        {
            // Arrange
            var client = new ClientDto { Id = Guid.NewGuid(), Name = "Test Client" };
            var dbclient = new Client { Id = client.Id, Name = client.Name, CreatedDate = DateTime.Now };
            _mockMapper.Setup(x => x.Map<Client>(client)).Returns(dbclient);
            _mockClientService.Setup(x => x.CreateClient(dbclient)).ReturnsAsync(dbclient);

            // Act
            var result = await _controller.Post(client) as CreatedAtActionResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(dbclient, result.Value);
            Assert.Equal("Get", result.ActionName);
            Assert.Equal(client.Id, result.RouteValues["id"]);
        }

        [Fact]
        public async void Put_WithValidId_Returns200OK()
        {
            // Arrange
            var client = new ClientDto { Id = Guid.NewGuid(), Name = "Test Client" };
            var dbclient = new Client { Id = client.Id, Name = client.Name };
            _mockMapper.Setup(x => x.Map<Client>(client)).Returns(dbclient);
            _mockClientService.Setup(x => x.GetClientAsync(client.Id)).ReturnsAsync(dbclient);
            _mockClientService.Setup(x => x.UpdateClient(dbclient)).ReturnsAsync(dbclient);
            _mockMapper.Setup(x => x.Map<ClientDto>(dbclient)).Returns(client);

            // Act
            var result = await _controller.Put(client.Id, client) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(client, result.Value);
        }

        [Fact]
        public async Task Put_WithInvalidId_Returns400BadRequest()
        {
            // Arrange
            var clientDto = new ClientDto { Id = Guid.NewGuid(), Name = "Test Client" };
            var invalidId = Guid.NewGuid();

            

            // Act
            var result = await _controller.Put(invalidId, clientDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task Delete_ReturnsOk_WhenDeletionSucceeds()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock.Setup(x => x.DeleteClient(It.Is<Client>(c => c.Id == clientId)))
                .ReturnsAsync(true);
            var mapperMock = new Mock<IMapper>();
            var controller = new ClientController(clientServiceMock.Object, mapperMock.Object);

            // Act
            var result = await controller.Delete(clientId);

            // Assert
            Assert.IsType<OkResult>(result);
            clientServiceMock.Verify(x => x.DeleteClient(It.Is<Client>(c => c.Id == clientId)), Times.Once);
        }
        [Fact]
        public async Task Get_ReturnsOk_WhenClientExists()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            var mapperMock = new Mock<IMapper>();
            var client = new Client { Id = Guid.NewGuid() };
            var clientDto = new ClientDto { Id = client.Id };
            clientServiceMock.Setup(x => x.GetClientAsync(client.Id)).ReturnsAsync(client);
            mapperMock.Setup(x => x.Map<ClientDto>(client)).Returns(clientDto);
            var controller = new ClientController(clientServiceMock.Object, mapperMock.Object);

            // Act
            var result = await controller.Get(client.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedClient = Assert.IsType<ClientDto>(okResult.Value);
            Assert.Equal(clientDto.Id, returnedClient.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenClientDoesNotExist()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            var mapperMock = new Mock<IMapper>();
            var clientId = Guid.NewGuid();
            clientServiceMock.Setup(x => x.GetClientAsync(clientId)).ReturnsAsync((Client)null);
            var controller = new ClientController(clientServiceMock.Object, mapperMock.Object);

            // Act
            var result = await controller.Get(clientId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }







}


