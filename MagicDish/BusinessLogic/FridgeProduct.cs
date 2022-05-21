using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FridgeProduct : Product
    {
        public FridgeProduct(string name, UnitOfMeasure unitOfMeasure) : base(name, unitOfMeasure)
        {
        }

        public FridgeProduct(string name, UnitOfMeasure unitOfMeasure, int quantity) : base(name, unitOfMeasure, quantity)
        {
        }
    }
}
