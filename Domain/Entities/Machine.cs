using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Machine:Entity
    {
        protected Machine()
        {

        }
        public Machine(int id)
        {
        }
        public Machine(string? number, string name, List<MediaUrl> mediaUrls)
        {
            Number = number;
            Name = name;
            MediaUrls = mediaUrls;
        }

        public string? Number { get; private set; } = null;
        public string Name { get; private set; }
        public List<MediaUrl> MediaUrls { get; private set; }
        public List<Exercise> Exercises { get; private set; }

        public void UpdateAll(Machine machine)
        {
            MediaUrls.Clear();
            Number = machine.Number;
            Name = machine.Name;
            MediaUrls = machine.MediaUrls;
        }
    }
}
