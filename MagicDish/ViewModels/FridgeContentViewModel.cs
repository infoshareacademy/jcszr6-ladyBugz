﻿using MagicDish.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MagicDish.Web.ViewModels
{
    public class FridgeContentViewModel
    {
        public string ProductCategory { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public bool IsVegan { get; set; }
        public SelectList ProductCategoriesDropdown { get; set; }
        public SelectList ProductsDropdown { get; set; }
        public SelectList UnitsOfMeasureDropdown { get; set; }
    }
}
