using System.ComponentModel.DataAnnotations;


namespace MagicDish.Core.Models
{
    public class FridgeProduct : ProductQuantity
    {
        [Key]
        public int Id { get; set; }
        public int FridgeId { get; set; }
    }
}
