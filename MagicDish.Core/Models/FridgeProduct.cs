using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class FridgeProduct
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public int FridgeId { get; set; }

        [ForeignKey("FridgeId")]
        public Fridge Fridge { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
