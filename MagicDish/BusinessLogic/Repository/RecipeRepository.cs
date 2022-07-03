using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        static List<Recipe> _recipes = new List<Recipe>
        {
            new Recipe(1,"cheese tostie", 10, true,
                new List<ProductQuantity>
                {
                    new ProductQuantity(new Product(4, "bread", UnitOfMeasure.pieces), 2),
                    new ProductQuantity(new Product(5, "cheese", UnitOfMeasure.grams), 100)
                },
                "put cheese inside bread, warm up"),
        };
        public Task<List<Recipe>> GetAsync()
        {
            return Task.FromResult<List<Recipe>>(_recipes);
        }

    }
}
