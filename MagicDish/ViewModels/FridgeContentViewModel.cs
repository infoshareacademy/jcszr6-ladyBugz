using MagicDish.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MagicDish.Web.ViewModels
{
    public class FridgeContentViewModel
    {
        [Required(ErrorMessage = "Please select Product Category")]
        public string ProductCategory { get; set; }
        [Required(ErrorMessage = "Please select Product")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please add valid amount. The value has to be an integer between 0 and 10000")]
        [Range(0, 10000)]
        public int Amount { get; set; }
        [Required]
        public string Unit { get; set; }
        public bool IsVegan { get; set; }
        public SelectList ProductCategoriesDropdown { get; set; }
        public SelectList ProductsDropdown { get; set; }
        public SelectList UnitsOfMeasureDropdown { get; set; }
    }
}
