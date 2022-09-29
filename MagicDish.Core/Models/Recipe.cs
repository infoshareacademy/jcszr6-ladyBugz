using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDish.Core.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Product> Ingredients { get; set; }
        [Required]
        public int CookingTimeInMinutes { get; set; }
        [Required]
        public bool IsVegeterian { get; set; }
        [Required]
        public string Description { get; set; }
    }
}