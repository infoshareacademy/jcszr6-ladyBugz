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
    }
}
