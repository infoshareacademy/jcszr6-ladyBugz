using BusinessLogic;
using MagicDishWebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDishWebApplication.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private MagicDishWebApplicationContext _context;
        public RecipeRepository(MagicDishWebApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Recipe> GetAll()
        {
            return (IEnumerable<Recipe>)_context.Recipes.AsEnumerable();
        }
    }
}