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
