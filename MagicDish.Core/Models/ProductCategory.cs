﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Core.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
