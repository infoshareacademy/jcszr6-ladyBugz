﻿using BusinessLogic;
using MagicDishWebApplication;

namespace MagicDishWebApplication.Models
{
    public class FoodRepositoryProductModel
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
