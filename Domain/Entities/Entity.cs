using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {

        }
        [Key]
        public int Id { get; private set; }
        public void SetId(int id) => Id = id;
    }
}
