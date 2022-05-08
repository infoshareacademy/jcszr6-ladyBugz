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
        public uint UserId { get; set; }
        public string Username { get; set; }


        private List<UserAccount> users = new List<UserAccount>();

        public UserAccount(string name, string surname, string email, uint UserID, string username)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UserId = UserID;
            Username = username;
        }

        public void AddUser()
        {
           

    }
    
}
