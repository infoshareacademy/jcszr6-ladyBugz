using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class Fridge
    {
        [Key]
        public int Id { get; set; }

        public IList<FridgeProduct> Products { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }  
    }
}