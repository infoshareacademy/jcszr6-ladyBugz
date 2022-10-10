using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string UnitName { get; set; }
    }
}
