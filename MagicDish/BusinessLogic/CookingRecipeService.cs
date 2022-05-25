using Newtonsoft.Json;

namespace BusinessLogic
{
    public class CookingRecipeService
    {
        public static List<CookingRecipe> Recipes {
            get { return new List<CookingRecipe>(); }
            set
            {
                var recipesContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Recipes.json"));
                CookingRecipeService.Recipes = JsonConvert.DeserializeObject<List<CookingRecipe>>(recipesContent);
            } 
        }


        public static List<CookingRecipe> GetCookingRecipes()
        {
            return Recipes;
        }

        public static void AddUser(CookingRecipe recipe)
        {
            Recipes.Add(recipe);
        }
    }
}
