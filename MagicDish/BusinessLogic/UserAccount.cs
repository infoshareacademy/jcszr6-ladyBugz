using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserAccount
    {
        private string _name;
        private string _surname;
        private string _email;
        //private string _userId; - how to set this one?
       private string _username;

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                bool valid;
                do
                {
                    string? input = value;
                    if (!string.IsNullOrEmpty(input))
                    {
                        _name = input;
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("Invalid input, try again");
                        Console.WriteLine("What is your name?");
                        input = Console.ReadLine();
                    }
                }
                while (!valid);
            }
        }

        public String Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                bool valid;
                do
                {
                    string? input = value;
                    if (!string.IsNullOrEmpty(input))
                    {
                        _surname = input;
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("Invalid input, try again");
                        Console.WriteLine("What is your surname?");
                        input = Console.ReadLine(); 
                    }
                }
                while (!valid);
            }
        }

        public String Email
        //TO DO: validate email format (using regex)?
        {
            get
            {
                return _email;
            }
            set
            {
                bool valid;
                do
                {
                    string? input = value;
                    if (!string.IsNullOrEmpty(input))
                    {
                        _email = input;
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("Invalid input, try again");
                        Console.WriteLine("What is your email?");
                        input = Console.ReadLine();
                    }
                }
                while (!valid);
            }
        }

        public String Username
        //TO DO: check if username hasn't been user before
        {
            get
            {
                return _username;
            }
            set
            {
                bool valid;
                do
                {
                    string? input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        _username = input;
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("Invalid input, try again");
                        Console.WriteLine("Enter your username");
                    }
                }
                while (!valid);
            }
        }

        public Fridge Fridge { get; private set; } // jesli user moze miec kilka lodowek to tutaj musi byc lista

        public UserAccount()
        {
        }

        public UserAccount(string name, string surname, string email, int UserID, string username)
        {
            Name = name;
            Surname = surname;
            Email = email;
            //UserId = UserID;
            Username = username;
        }
    }
}
