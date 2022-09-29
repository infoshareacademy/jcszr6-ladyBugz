using System.ComponentModel.DataAnnotations;


namespace MagicDish.Core.Models
{
    public class Ingridient : Product
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
    }
}
