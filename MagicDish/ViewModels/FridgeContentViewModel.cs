using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MagicDish.Web.ViewModels
{
    public class FridgeContentViewModel
    {
        public string Name { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public bool IsVegan { get; set; }
        public int Amount { get; set; }
    }
}
