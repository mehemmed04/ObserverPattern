using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    public class User
    {
        public List<User> Subscribers { get; set; } = new List<User>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
