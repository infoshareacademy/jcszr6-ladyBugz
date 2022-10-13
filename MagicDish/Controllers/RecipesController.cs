using System.Security.Claims;
using System.Text.Json.Serialization;
using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace MagicDish.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = _context.AvailableProducts;

            var results = from fridge in _context.Fridges
                          join fridgeProduct in _context.FridgeProducts on fridge.Id equals fridgeProduct.FridgeId
                          join product in _context.AvailableProducts on fridgeProduct.ProductId equals product.Id
                          where (fridge.UserId == userId)
                          select new { ProductName = product.Name };

            var fridgeProductResult = results.ToList();

            var allRecipes = new List<RecipeDto>();

            foreach (var fridgeProduct in fridgeProductResult)
            {
                var recipeResponse = await GetAllRecipes(fridgeProduct.ProductName);
                allRecipes.AddRange(recipeResponse);
            }

            var filteredRecipes = allRecipes.GroupBy(x => x.Id).OrderByDescending(y => y.Count()).ToList();

            List<RecipeViewModel> recipesToDisplay = new(
                filteredRecipes.Select(r => new RecipeViewModel()
                {
                    Id = r.Select(r => r.Id).FirstOrDefault(),
                    Name = r.Select(r => r.Name).FirstOrDefault(),
                    RecipeExternalLink = $"https://tasty.co/recipe/{r.Select(r => r.Url).FirstOrDefault()}"
                }));

            foreach (var recipe in recipesToDisplay)
            {
                recipe.Ingredients = await GetAllInredientsForRecipe(recipe.Id);
            }

            return View(recipesToDisplay);
        }

        public class ApiGetListResponse
        {
            [JsonPropertyName("count")]
            public int Count { get; set; }

            [JsonPropertyName("results")]
            public List<RecipeDto> Results { get; set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class RecipeDto
        {
            [JsonPropertyAttribute("id")]
            public int Id { get; set; }

            [JsonPropertyAttribute("name")]
            public string Name { get; set; }

            [JsonPropertyAttribute("slug")]
            public string Url { get; set; }
        }

        private async Task<List<RecipeDto>> GetAllRecipes(string ingridientName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://tasty.p.rapidapi.com/recipes/list?from=0&size=40&q={ingridientName}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "56f5ad242bmshdd3ef9e7875c7e2p1c7ad4jsnb979b7d20deb" },
                    { "X-RapidAPI-Host", "tasty.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiGetListResponse>(body);
                return result.Results;
            }
        }

        private async Task<List<String>> GetAllInredientsForRecipe(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://tasty.p.rapidapi.com/recipes/get-more-info?id={id}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "56f5ad242bmshdd3ef9e7875c7e2p1c7ad4jsnb979b7d20deb" },
                    { "X-RapidAPI-Host", "tasty.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiGetInfoResponse>(body);
                //List<String> ingredients = result.Sections.Where(c => c.Components is not null).Select(c => c.Components.Where(i => i.Ingredient is not null).FirstOrDefault().Ingredient?.ToString()).ToList();
                var ingredients = result.Sections.Where(s => s.Components is not null).Select(c => c.Components.First().Ingredient?.ToString()).ToList();
                return ingredients;
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class ApiGetInfoResponse
        {
            [JsonPropertyName("sections")]
            public List<Section> Sections { get; set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Section
        {
            [JsonPropertyName("components")]
            public List<Component> Components { get; set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Component
        {
            [JsonPropertyAttribute("raw-text")]
            public string Ingredient { get; set; }
        }
    }
}