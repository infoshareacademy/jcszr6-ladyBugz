using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public Product(int id, string name, UnitOfMeasure unitOfMeasure)
        {
            Id = id;
            Name = name;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
