using ClientManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientManagementSystem.Data
{
    public class CMSDbContext : DbContext
    {
        public CMSDbContext()
        {
        }
        public CMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
