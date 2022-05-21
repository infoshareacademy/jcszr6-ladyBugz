using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Fridge
    { 

        // czy trzeba rozroznic product z lodowki od productu z recipe
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Fridge()
        {
        }

        public Fridge(string name)
        {
            Name = name;
        }

        public void AddProductToFridge(Product product)
        {
            Products.Add(product);
        }
    }
}
