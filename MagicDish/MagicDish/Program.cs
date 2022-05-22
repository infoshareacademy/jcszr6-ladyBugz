using BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Milk",
                    Quantity = 1000,
                    UnitOfMeasure = UnitOfMeasure.Mililiters
                },
                new Product
                {
                    Name = "Cereal",
                    Quantity = 300,
                    UnitOfMeasure = UnitOfMeasure.Grams
                },
                new Product
                {
                    Name = "Carrot",
                    Quantity = 3,
                    UnitOfMeasure = UnitOfMeasure.Pieces
                },
                new Product
                {
                    Name = "Potatoes",
                    Quantity = 1200,
                    UnitOfMeasure = UnitOfMeasure.Grams
                },
                new Product
                {
                    Name = "Rice",
                    Quantity = 500,
                    UnitOfMeasure = UnitOfMeasure.Grams
                },
                new Product
                {
                    Name = "Chicken",
                    Quantity = 350,
                    UnitOfMeasure = UnitOfMeasure.Grams
                }
            };
            var user = new UserAccount
            {
                UserId = 1,
                Username = "test-user",
                Email = "test-user@magicdish.com",
                Name = "John",
                Surname = "Doe",
                FoodRepository = new FoodRepository
                {
                    Id = 1,
                    Name = "John Doe's Test FoodRepository",
                    Products = products
                }
            };

            var recipe = new CookingRecipe
            {
                CookingTimeInMinutes = 2,
                Name = "Milk with Cereal",
                RequiredProducts = new List<Product>
                {
                    new Product
                    {
                        Name = "Milk",
                        UnitOfMeasure = UnitOfMeasure.Mililiters,
                        Quantity = 250
                    },
                    new Product
                    {
                        Name = "Cereal",
                        UnitOfMeasure = UnitOfMeasure.Grams,
                        Quantity = 80
                    }
                }
            };

            var programData = new ProgramData();
            programData.Recipes = new List<CookingRecipe>();
            programData.Recipes.Add(recipe);

            programData.Users = new List<UserAccount>();
            programData.Users.Add(user);

            //konwertuje dane z klasy propgram data do jsonstring
            //var jsonString = JsonConvert.SerializeObject(programData);

            //wczytuje dane tekstowe z pliku json do zmiennej fileContents
            //var fileContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));

            //wrzuca dane ze zmiennej fileContents do klasy ProgramData
            //var data = JsonConvert.DeserializeObject<ProgramData>(fileContents);

        }

    }
}
