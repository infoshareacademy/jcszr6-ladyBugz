using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CookingRecipeService
    {
        public static List<CookingRecipe> Recipes = new List<CookingRecipe>
        {
            new CookingRecipe()};

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
