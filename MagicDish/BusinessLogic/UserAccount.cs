using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserAccount
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public Fridge Fridge { get; set; }

        public UserAccount()
        {

        }

        public UserAccount(string name, string surname, string email, int UserID, string username)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UserId = UserID;
            Username = username;
        }
    }
}
