using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MagicDish.Web.ViewModels
{
    public class FridgeContentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select the product category")]
        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }

        [Required(ErrorMessage = "Please select the product")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter a valid amount")]
        [Range(1, 10000, ErrorMessage = "The amount value has to be an integer between 0 and 10000")]
        public int Amount { get; set; }

        [Required]
        public string Unit { get; set; }

        [DisplayName("Is vegan?")]
        public bool IsVegan { get; set; }

        public SelectList ProductCategoriesDropdown { get; set; }

        public SelectList ProductsDropdown { get; set; }

        public SelectList UnitsOfMeasureDropdown { get; set; }
    }
}
