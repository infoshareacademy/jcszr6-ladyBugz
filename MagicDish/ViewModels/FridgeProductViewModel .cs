using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MagicDish.Web.ViewModels
{
    public class FridgeProductViewModel
    {
        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter a valid amount")]
        [Range(1, 10000, ErrorMessage = "The amount value has to be an integer between 1 and 10000")]
        public int Amount { get; set; }

        public string Unit { get; set; }
    }
}
