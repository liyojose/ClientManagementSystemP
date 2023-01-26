using ClientManagementSystem.Data;
using ClientManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.Common.Services
{
    public interface IClientService
    {
        Task<Client> CreateClient(Client client);
        Task<Client> DeleteClient(Client client);
        Task<Client> GetClientAsync(Guid id);
        Task<List<Client>> GetClientsAsync();
        Task<Client> UpdateClient(Client client);
    }

    public class ClientService : IClientService
    {
        private readonly CMSDbContext _dataContext;

        public ClientService(CMSDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> GetClientAsync(Guid id)
        {
            return await _dataContext.Clients.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _dataContext.Clients.ToListAsync();
        }
        public async Task<Client> CreateClient(Client client)
        {
            await _dataContext.Clients.AddAsync(client);
            await _dataContext.SaveChangesAsync();
            return client;
        }
        public async Task<Client> UpdateClient(Client client)
        {
            _dataContext.Clients.Update(client);
            await _dataContext.SaveChangesAsync();
            return client;
        }

        public async Task<Client> DeleteClient(Client client)
        {
            _dataContext.Clients.Remove(client);
            await _dataContext.SaveChangesAsync();
            return client;
        }
    }
}
