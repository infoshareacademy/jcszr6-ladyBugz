using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserAccount
    {
        //private int _userId; - how to set this one?

        public String Name{ get; set; }
        public String Email{ get; set; }
        public String Username { get; set; }
       

        public FoodRepository FoodRepository { get; set; } 

        public UserAccount()
        {
        }

        public UserAccount(string name, string surname, string email, int UserID, string username)
        {
            Name = name;
            Email = email;
            Username = username;
        }
    }
}
