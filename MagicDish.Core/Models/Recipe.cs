using System.ComponentModel.DataAnnotations;

namespace MagicDish.Core.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string RecipeExternalLink { get; set; }
    }
}