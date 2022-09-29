using System.ComponentModel.DataAnnotations;

namespace MagicDish.Core.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingridient> Ingredients { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public string Description { get; set; }
    }
}