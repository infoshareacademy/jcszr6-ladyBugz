using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FoodRepository
    { 

        public string Name { get; set; }
        public List<ProductQuantity> Products { get; set; }

        
        public FoodRepository()
        {
            Products = new List<ProductQuantity>();
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




    }
}
