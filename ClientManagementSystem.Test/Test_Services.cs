using ClientManagementSystem.Common.Services;
using ClientManagementSystem.Data;
using ClientManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ClientManagementSystem.Test
{
    public class Test_Services
    {

        [Fact]
        public async Task GetClientsAsync_ReturnsClients()
        {
            // Arrange
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
             service.Setup(x => x.GetClientsAsync().Result)
                .Returns(GetSampleClients());
            // Act
            var result = await service.Object.GetClientsAsync();

            // Assert
            Assert.True( result.Count > 0 );
        }
        [Fact]
        public async Task GetClientAsync_ReturnsCorrectClient()
        {
            // Arrange
            var searchclientId = Guid.Parse("4d62f905-5d3a-4bf8-a281-0fed158fe200");
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
            service.Setup(x => x.GetClientAsync(searchclientId).Result)
               .Returns(GetSampleClients()[0]);
            // Act
            var result = await service.Object.GetClientAsync(searchclientId);

            // Assert
            Assert.Equal(result.Id, searchclientId);
        }
        [Fact]
        public async Task CreateClientAsync_CreateCorrectClient()
        {
            // Arrange
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
            service.Setup(x => x.CreateClient(clients[0]).Result)
               .Returns(GetSampleClients()[0]);
            // Act
            var result = await service.Object.CreateClient(clients[0]);
            // Assert
            Assert.Equal(result.Id, clients[0].Id);
        }
        [Fact]
        public async Task UpdateClientAsync_UpdateCorrectClient()
        {
            // Arrange
            var clienttoupdate = new Client
            {
                Id = Guid.Parse("5d62f905-5d3a-4bf8-a281-0fed158fe200"),
                CreatedDate = DateTime.Now,
                Email = "update@gmail.com",
                Name = "update"
            };
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
            service.Setup(x => x.UpdateClient(clienttoupdate).Result)
               .Returns(clienttoupdate);
            // Act
            var result = await service.Object.UpdateClient(clienttoupdate);
            // Assert
            Assert.Equal(result.Id, clienttoupdate.Id);
        }
        [Fact]
        public async Task DeleteClientAsync_UpdateCorrectClient()
        {
            // Arrange
            var clienttoupdate = new Client
            {
                Id = Guid.Parse("5d62f905-5d3a-4bf8-a281-0fed158fe200"),
                CreatedDate = DateTime.Now,
                Email = "update@gmail.com",
                Name = "update"
            };
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
            service.Setup(x => x.DeleteClient(clienttoupdate).Result)
               .Returns(true);
            // Act
            var result = await service.Object.DeleteClient(clienttoupdate);
            // Assert
            Assert.True(result);
        }

        private List<Client> GetSampleClients()
        {
            List<Client> output = new List<Client>
        {
            new Client
            {Id = Guid.Parse("4d62f905-5d3a-4bf8-a281-0fed158fe200"),
                CreatedDate = DateTime.Now,
                Email = "jhon@gmail.com",
                Name = "Client"
            }
            };
            return output;
        }
    }
}
