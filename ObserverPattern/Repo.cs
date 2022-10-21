using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    public class Repo
    {

        public List<User> Users { get; set; }
        public Repo()
        {
            Users = new List<User>();
            if (File.Exists("Users.json"))
            {
                Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json"));
            }
        }
        public void SetUsersFromFile()
        {
            Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json"));
        }
        public User GetUserByUsername(string Username)
        {
            return Users.SingleOrDefault(x => x.Username == Username);
        }
        public void AddUser(User u)
        {
            Users.Add(u);
            string fileName = "Users.json";
            string jsonString = JsonConvert.SerializeObject(Users);
            File.WriteAllText(fileName, jsonString);
        }
        public void Update()
        {
            string fileName = "Users.json";
            string jsonString = JsonConvert.SerializeObject(Users);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
