using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MagicDish.Web.ViewModels
{
    public class FridgeProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Range(1, 10000, ErrorMessage = "The amount value has to be an integer between 0 and 10000")]
        public int Amount { get; set; }

        public string Unit { get; set; }
    }
}


