using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDish.Core.Models
{
    public class Fridge
    {
        [Key]
        public int Id { get; set; }

        public IList<ProductQuantity> Products { get; set; }

        public int UserId { get; set; }
    }
}