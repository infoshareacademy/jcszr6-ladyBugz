using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProductService
    {
        private static List<Product> Products = new List<Product>
            {
                new Product(1, "milk", UnitOfMeasure.Mililiters),
                new Product(2, "wheat", UnitOfMeasure.Grams),
                new Product(3, "egg", UnitOfMeasure.Pieces),
                new Product(4, "sugar", UnitOfMeasure.Grams)
            };

        //tutaj bedzie pobieranie z pliku tekstowego
        public static List<Product> GetProducts()
        {
            return Products;
        }
    }
}
