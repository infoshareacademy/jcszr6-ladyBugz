using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Menu
    {
        public static void WelcomeMenu()
        {

            Console.WriteLine("Welcome to your food-repository!");
            Console.WriteLine("Are you an existing user or would you like to register?");
            Console.WriteLine("1 - I'm a registered user");
            Console.WriteLine("2 - I'd like to register for the first time");
            int option = CollectInputAsValidOption(2);
            switch(option)
            {
                case 1: 
                    SignInMenu();
                    break;
                case 2: 
                    SignUpMenu();
                    break;       
            }

        }

        private static void SignInMenu()
        {
            //TO DO: sign in menu az do strony twoja lodowka
        }

        private static void SignUpMenu()
        {
            UserAccount user = new UserAccount();
            Console.WriteLine("First things first. What is your name?");
            user.Name = CollectStringInput("name");
            Console.WriteLine("Now enter your user name");
            user.Username = CollectStringInput("username");
            Console.WriteLine("What is your email?");
            user.Email = CollectEmailInput();
            Console.WriteLine($"Nice to meet you {user.Name}");
            Console.WriteLine("Let's create your first food-repository");
            Console.WriteLine("How would you like to name your food-repository?");
            user.FoodRepository = new FoodRepository(CollectStringInput("fridge"));
            Console.WriteLine($"Now, add first product to your {user.FoodRepository}");
            FoodRepositoryMenu(user.FoodRepository);
        }

        private static void FoodRepositoryMenu(FoodRepository foodRepository)
        {
            // tutaj bedziemy drukowac opcje z wczytanego pliku, ale na razie z listy

            Console.WriteLine("Select product to add from the list:");

            var products = ProductService.GetProducts();

            for (int i = 0; i < products.Count(); i++)
            {
                Console.WriteLine($"{i+1} - {products[i]}");
            }

            int option = CollectInputAsValidOption(products.Count);
            Console.WriteLine($"How much/many of the {products[option - 1].Name} would you like to add to the repo? [{products[option - 1].UnitOfMeasure.ToString()}]");
            int quantity = CollectUnitInput();
            foodRepository.AddProductToFoodRepository(products[option - 1], quantity);

            //okejka tutaj juz grubo polecialam. czy wybrany z listy dostepnych produktow produkt dodaje do lodowki, czy nowy produkt o tych samych wlasciwosciach?
            

        }

        private static int CollectUnitInput()
        {
            bool valid;
            int unit = -1;

            do
            {
                string? input = Console.ReadLine();
                bool inputIsInt = Int32.TryParse(input, out int number);
                if (inputIsInt && number > 0)
                {
                    unit = number;
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again");
                }
            }
            while (!valid);
            return unit;
        }

        private static int CollectInputAsValidOption(int numberOfPossibleOptions)
        {

            bool valid;
            int option = -1;

            int[] possibleOptions = new int[numberOfPossibleOptions];
            for (int i = 0; i < numberOfPossibleOptions; i++)
            {
                possibleOptions[i] = i+1;
            }

            // sprawdzic czy input mniejszy rowny od liczby mozliwych opcji

            do
            {
                string? input = Console.ReadLine();
                bool inputIsInt = Int32.TryParse(input, out int number);
                bool inputIsContainedWithinPossibleOptions = possibleOptions.Contains(number);

                if (inputIsInt && inputIsContainedWithinPossibleOptions)
                {
                    option = number;
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again");
                }
            }
            while (!valid);

            return option;
        }

        private static string CollectStringInput(string name)
        {

            //collect string input - przyjmij nazwe inputu jako parametr
            bool valid;
            string value = "";

            do
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    value = input;
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again");
                    Console.WriteLine($"Please, enter a valid {name}");
                }
            }
            while (!valid);

            return value;
        }

        private static string CollectEmailInput()
        {
            bool valid;
            string email = "";

            do
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    email = input;
                    valid = true;
                }
                //TO DO: sprawdz czy jest malpka 
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again");
                    Console.WriteLine("Please, enter you email address");
                }
            }
            while (!valid);

            return email;
        }
    }
}
