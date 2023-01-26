using ClientManagementSystem.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.Data.Models
{
    public class Client : BaseEntity<Guid>, IEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
