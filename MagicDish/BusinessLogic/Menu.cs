using Newtonsoft.Json;

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
            switch (option)
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

            UserAccount user = new UserAccount();
            var usersList = new List<UserAccount>();
            string usersDataPath = @"C:\Users\%username%\Desktop\Projekt grupowy\MagicDish\MagicDish\bin\Debug\net6.0\Users";
            var fileContent = File.ReadAllText(Path.Combine(usersDataPath, "Users.json"));
            usersList = JsonConvert.DeserializeObject<List<UserAccount>>(fileContent);
            Console.WriteLine("Hello, please tell me your username");
            user.Username = CollectStringInput("username");

            var usernames = usersList.Select(u => u.Username);
            while
                (!usernames.Contains(user.Username))
            {
                Console.WriteLine("Invalid username. Please enter username again:");
                user.Username = CollectStringInput("username");
            }
            Console.WriteLine("Hello, please tell me your password");
            user.Password = CollectStringInput("password");



            //napisac kod aby pobral password zalogowanego usera z loggedInUser
            //var loggedInUser = usersList.Select(u => u).Where(u => u.Username == user.Username);   <<< to nie dziala
            //var password = loggedInUser.Select(p => p.Password);   <<< to nie dziala
            //var usersPassword = loggedInUser.Select(u => u.Password).ToList();      <<< to nie dziala

            //var passwords = usersList.Select(u => u.Password);

            
            //while (!passwords.Contains(user.Password))
            //{
            //    Console.WriteLine("Invalid password. Please enter password again:");
            //    user.Username = CollectStringInput("password");
            //}

            



            

        }

        private static void SignUpMenu()
        {
            UserAccount user = new UserAccount();
            var usersList = new List<UserAccount>();
            string usersDataPath = Environment.CurrentDirectory + @"\Users";

            
            //napisac cos na pierwszy rozruch poniewaz przy 1szym starcie programu nie ma
            //pliku "Users" jeszcze utworzonego i wali exception

                var fileContent = File.ReadAllText(Path.Combine(usersDataPath, "Users.json"));
                usersList = JsonConvert.DeserializeObject<List<UserAccount>>(fileContent);
            

            if
                (usersList.Count() == 0)
            {
                user.Id = 0;
            }
            else
            {
                user.Id = usersList.Count();
            }   
                Console.WriteLine("First things first. What is your name?");
                user.Name = CollectStringInput("name");
                Console.WriteLine("Now enter your user name");
                user.Username = CollectStringInput("username");
                var usernames = usersList.Select(u => u.Username);
                while (usernames.Contains(user.Username))
                {
                    Console.WriteLine("username already taken, please chose different username:");
                    user.Username = CollectStringInput("username");
                }
                Console.WriteLine("Create a password");
                user.Password = CollectStringInput("password");
                Console.WriteLine("What is your email?");
                user.Email = CollectEmailInput();
                Console.WriteLine($"Nice to meet you {user.Name}");
                Console.WriteLine("Let's create your first food-repository");
                Console.WriteLine("How would you like to name your food-repository?");
                user.FoodRepository = new FoodRepository(CollectStringInput("fridge"));
                Console.WriteLine($"Now, add first product to your {user.FoodRepository.Name}");
                string wantToAddanotherProduct;
                do
                {
                    AvailableProductsMenu(user.FoodRepository);
                    Console.WriteLine("Would you like to add another product?");
                    wantToAddanotherProduct = CollectYesOrNoInput();

                }
                while (wantToAddanotherProduct == "yes");
                FoodRepositoryMenu(user.FoodRepository);
                usersList.Add(user);
                var usersData = JsonConvert.SerializeObject(usersList, Formatting.Indented);

                File.WriteAllText(Path.Combine(usersDataPath, "Users.json"), usersData);


                var foodRepoData = JsonConvert.SerializeObject(user.FoodRepository, Formatting.Indented);
                string foodReposPath = @"C:\Users\skier\Desktop\Projekt grupowy\MagicDish\MagicDish\bin\Debug\net6.0\FoodRepositories";
                File.WriteAllText(Path.Combine(foodReposPath, $"{user.FoodRepository.Name}.json"), foodRepoData);
            
        }

        private static void FoodRepositoryMenu(FoodRepository foodRepository)
        {
            Console.WriteLine($"You're in {foodRepository.Name}");
            Console.WriteLine("Products available:");
            foreach (var product in foodRepository.Products)
            {
                Console.WriteLine($"{product.Product.Id} - {product.Quantity} [{product.Product.UnitOfMeasure}] of {product.Product.Name}");
            }
            Console.WriteLine("What would you like to do from here?");
            Console.WriteLine($"1 - Add more products to {foodRepository.Name}");
            Console.WriteLine("2 - Edit available products");
            Console.WriteLine("3 - Remove product from the repo");
            Console.WriteLine("4 - Search for a recipe based on food you have in your repo");
            Console.WriteLine("5 - See all the recipes");
            Console.WriteLine("6 - Exit application");
        }

        private static string CollectYesOrNoInput()
        {
            bool valid;
            string value = "";

            do
            {
                string? input = Console.ReadLine();
                if (input == "yes" || input == "no")
                {
                    value = input;
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again ('yes' for yes, 'no' for no)");
                }
            }
            while (!valid);

            return value;
        }

        private static void AvailableProductsMenu(FoodRepository foodRepository)
        {
            // tutaj bedziemy drukowac opcje z wczytanego pliku, ale na razie z listy
            var productsContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Products.json"));
            ProductService.Products = JsonConvert.DeserializeObject<List<Product>>(productsContent);

            Console.WriteLine("Select product to add from the list:");
            

            var products = ProductService.GetProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name}");
            }


            Product selectedProductOption = CollectProductById(products);
            Console.WriteLine($"How much/many of {selectedProductOption.Name} would you like to add to the repo? [{selectedProductOption.UnitOfMeasure}]");
            int quantity = CollectUnitInput();
            foodRepository.AddProductToFoodRepository(selectedProductOption, quantity);
            Console.WriteLine($"{selectedProductOption.Name} added to the food repository");
        }

        private static Product CollectProductById(List<Product> products)
        {
            bool valid;
            Product? product;

            do
            {
                string? input = Console.ReadLine();
                bool inputIsInt = Int32.TryParse(input, out int number);
                product = products.FirstOrDefault(p => p.Id == number);

                if (product != null)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid input, try again");
                }
            }
            while (!valid);

            return product!;
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

            do
            {
                string? input = Console.ReadLine();
                bool inputIsInt = Int32.TryParse(input, out int number);
                bool inputIsContainedWithinPossibleOptions = number > 0 && number <= numberOfPossibleOptions;

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
                if (!string.IsNullOrEmpty(input) && input.Contains('@'))
                {
                    email = input;
                    valid = true;
                }
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
