using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RecipeProduct : Product
    {
        public RecipeProduct(string name, UnitOfMeasure unitOfMeasure) : base(name, unitOfMeasure)
        {
        }

        public RecipeProduct(string name, UnitOfMeasure unitOfMeasure, int quantity) : base(name, unitOfMeasure, quantity)
        {
        }
    }
}
