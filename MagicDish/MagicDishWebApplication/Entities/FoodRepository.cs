using MagicDishWebApplication.Models;
using System;
using System.Collections.Generic;
namespace BusinessLogic
{
    public class FoodRepository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductQuantity> Products { get; set; }

        public MagicDishWebApplicationUser MagicDishWebApplicationUser { get; set; }

        public FoodRepository()
        {

        }

        public FoodRepository(string name)
        {
            Products = new List<ProductQuantity>();
            Name = name;
        }

        public void AddProductToFoodRepository(Product product, int unit)
        {
            var newProduct = new ProductQuantity(product, unit);
            Products.Add(newProduct);
        }

        public List<ProductQuantity> GetProducts()
        {
            return Products;
        }
    }
}
