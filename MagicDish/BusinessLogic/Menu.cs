using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Menu
    {
        public void WelcomeMenu()
        {

            Console.WriteLine("Welcome to your food-repository!");

            //TO DO: zapytanie, czy chce sie dodac uzytkownika z pliku czy wprowadzic recznie + implementacja

            UserAccount user = new UserAccount();

            Console.WriteLine("First things first. What is your name?");
            user.Name = Console.ReadLine()!;
            Console.WriteLine("What is your surname?");
            user.Surname = Console.ReadLine()!;
            Console.WriteLine("What is your email?");
            user.Email = Console.ReadLine()!;
            Console.WriteLine("Please, set you username");
            Console.WriteLine($"Nice to meet you {user.Name}!");

            Console.WriteLine("Let's create your first food-repository, exciting!!!");
        }
    }
}
