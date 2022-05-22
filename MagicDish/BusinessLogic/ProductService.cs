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
                new Product("milk", UnitOfMeasure.Mililiters),
                new Product("wheat", UnitOfMeasure.Grams),
                new Product("egg", UnitOfMeasure.Pieces),
                new Product("sugar", UnitOfMeasure.Grams)
            };

        //tutaj bedzie pobieranie z pliku tekstowego
        public static List<Product> GetProducts()
        {
            return Products;
        }
    }
}
