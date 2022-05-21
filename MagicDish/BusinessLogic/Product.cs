using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Product
    {
        public string Name { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int Quantity { get; set; }

        //nie wiem czy potrzeba 2 konstruktorow, nie jestem pewna czy dziedziczenie just tutaj na miejscu
        public Product(string name, UnitOfMeasure unitOfMeasure)
        {
            Name = name;
            UnitOfMeasure = unitOfMeasure;
        }
        public Product(string name, UnitOfMeasure unitOfMeasure, int quantity)
        {
            Name = name;
            UnitOfMeasure = unitOfMeasure;
            Quantity = quantity;
        }
    }
}
