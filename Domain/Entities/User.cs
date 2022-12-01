using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Entity
    {
        public User(string name, string userNick, string pass, string profilePictureUrl)
        {
            Name = name;
            UserNick = userNick;
            Pass = pass;
            ProfilePictureUrl = profilePictureUrl;
        }

        protected User() { }
        public string Name { get; private set; }
        public string UserNick { get; private set; }
        public string Pass { get; private set; }
        public string ProfilePictureUrl { get; private set; }

    }
}
