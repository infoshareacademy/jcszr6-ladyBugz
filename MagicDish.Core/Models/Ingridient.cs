using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class Ingridient : Product
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        Recipe Recipe { get; set; } 
    }
}
