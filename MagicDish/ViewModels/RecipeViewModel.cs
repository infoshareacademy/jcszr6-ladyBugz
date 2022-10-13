using System.Text.Json.Serialization;
using MagicDish.Core.Models;

namespace MagicDish.Web.ViewModels
{

    public class RecipeViewModel
    {
		[JsonPropertyName("id")]
		public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }



        public List<string> ProductName { get; set; }


        public string RecipeExternalLink { get; set; }
    }
}
