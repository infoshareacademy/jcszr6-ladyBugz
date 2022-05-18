using BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace MagicDish
{
    public class Program
    {
        public static void Main(string[] args)

        { 
            
            UserAccount userAccount = new UserAccount(); 
            string jsonName, jsonFileName;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] answers = {"yes", "Yes", "y", "Y", "no", "No", "n", "N" };
            Console.WriteLine($"Hello user {userAccount.Name}");
            Console.WriteLine("Do you want to add any products to your Fridge from a file? yes/no");
            string userInput = Console.ReadLine();
            

            while (!answers.Contains(userInput))
            {
                IncorrectInput();
                userInput = Console.ReadLine();   
            }
            if (userInput == "yes" || userInput == "y" || userInput == "Yes" || userInput == "Y")
            {
                Console.Clear();
                Console.WriteLine("You have to paste the input file to your desktop. \n\nDo you want to open your desktop folder so you can paste the file there?");
                
                var answer = Console.ReadLine();

                while (!answers.Contains(answer))
                {
                    IncorrectInput();
                    answer = Console.ReadLine();
                }
                if (answer == "yes" || answer == "y" || answer == "Yes" || answer == "Y")
                {
                    Process.Start("explorer.exe", desktopPath);
                    Console.WriteLine("Desktop folder has been opened for you, please paste your input file there,once done press any key.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();
                }
                Console.WriteLine("Please tell me the name of your input file.");
                jsonName = Console.ReadLine();
                jsonFileName = jsonName + ".json";

                while (!File.Exists(Path.Combine(desktopPath, jsonFileName)))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have entered incorrect name of the file or the file doesn't exist on your desktop.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                    Console.WriteLine("Check is your file in desktop folder. Once done press any key");
                    Thread.Sleep(2000);
                    Process.Start("explorer.exe", desktopPath);
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Please tell me your file name (input is case sensitive):");
                    jsonName = Console.ReadLine();
                    jsonFileName = jsonName + ".json";
                }
                //get a path to the desktop for current user (Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                try
                {
                    
                    var fileContents = File.ReadAllText(Path.Combine(desktopPath, jsonFileName));
                    var data = JsonConvert.DeserializeObject<ProgramData>(fileContents);
                    ProgramData programData = new ProgramData();
                    programData.Users = data.Users;
                    

                    //foreach (var user in data.Users)
                    //{
                    //    Users.Add(user);
                    //}
                    
                    
                    //var programData = new ProgramData();
                    //programData.Recipes = new List<CookingRecipe>();  
                    Console.ReadKey();
                }
                catch (FileNotFoundException)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have entered incorrect name of the file or the file doesn't exist on your desktop.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                }
            }
            else
            { 
                Console.WriteLine();
            }  
            }


        static void IncorrectInput()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input, please try again");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        
       

        }
        }

    

