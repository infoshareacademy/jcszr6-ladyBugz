using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class FridgeProduct : ProductQuantity
    {
        [Key]
        public int Id { get; set; }

        public int FridgeId { get; set; }

        [ForeignKey("FridgeId")]
        public Fridge Fridge { get; set; }
    }
}
