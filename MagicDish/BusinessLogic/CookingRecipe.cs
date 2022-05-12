using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CookingRecipe
    {
        public string Name { get; set; }

        public List<Product> RequiredProducts { get; set; }

        public int CookingTimeInMinutes { get; set; }

        public bool IsVegeterian { get; set; }

    }
}
