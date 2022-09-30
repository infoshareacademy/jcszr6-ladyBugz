using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class Ingridient
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        Recipe Recipe { get; set; }
        
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
