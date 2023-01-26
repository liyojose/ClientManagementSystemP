using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.Data.Base
{
    public interface IEntity
    {
        object Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity
    {
        [Required]
        [Key]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
