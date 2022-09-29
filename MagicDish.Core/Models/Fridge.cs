﻿using System.ComponentModel.DataAnnotations;

namespace MagicDish.Core.Models
{
    public class Fridge
    {
        [Key]
        public int Id { get; set; }
        public IList<FridgeProduct> Products { get; set; }
        public int UserId { get; set; }
    }
}