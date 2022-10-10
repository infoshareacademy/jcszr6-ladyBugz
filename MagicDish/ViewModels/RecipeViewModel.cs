using MagicDish.Core.Models;

namespace MagicDish.Web.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string RecipeExternalLink { get; set; }
    }
}
