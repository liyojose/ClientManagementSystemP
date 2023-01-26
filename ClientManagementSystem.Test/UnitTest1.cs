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
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public async Task GetClientAsync_ReturnsCorrectClient()
        {
            // Arrange
            var service = new Mock<IClientService>();
            var clients = GetSampleClients();
            await service.Setup(x => x.GetClientsAsync())
                .Returns(GetSampleClients());


            var client = new Client { Id = Guid.NewGuid() };
            var dbSetMock = new Mock<DbSet<Client>>();
            dbSetMock.Setup(x => x.AsNoTracking()).Returns(dbSetMock.Object);
            //dbSetMock.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>())).ReturnsAsync(client);

            var contextMock = new Mock<CMSDbContext>();
            contextMock.Setup(x => x.Clients).Returns(dbSetMock.Object);

            // Act
            var result = await service.GetClientAsync(client.Id);

            // Assert
            Assert.Equal(client, result);
        }

        private List<Client> GetSampleClients()
        {
            List<Client> output = new List<Client>
        {
            new Client
            {Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Email = "jhon@gmail.com",
                Name = "Client"
            }
            };
            return output;
        }
    }
}
