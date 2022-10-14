using System.Security.Claims;
using System.Text.Json.Serialization;
using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.Helper;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;

namespace MagicDish.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
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

            int pageSize = 3;
            var paginatedRecipesToDisplay = PaginatedList<RecipeViewModel>.Create(recipesToDisplay, pageNumber ?? 1, pageSize);

            foreach (var recipe in paginatedRecipesToDisplay)
            {
                recipe.Ingredients = await GetAllInredientsForRecipe(recipe.Id);
            }

            return View(paginatedRecipesToDisplay);
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
                    { "X-RapidAPI-Key", "8a8f4e614cmsha810740f2d29ca6p1fd9c7jsn3c3a258fd3ba" },
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
                    { "X-RapidAPI-Key", "8a8f4e614cmsha810740f2d29ca6p1fd9c7jsn3c3a258fd3ba" },
                    { "X-RapidAPI-Host", "tasty.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiGetInfoResponse>(body);

                List<string> ingredients = new List<string>();
                foreach (Section section in result.Sections)
                {
                    List<string> list = section.Components.Select(i => i.IngredientData.IngredientName).ToList();
                    ingredients.AddRange(list);
                }

                return ingredients;
            }
        }

        [JsonObject]
        public class ApiGetInfoResponse
        {
            [JsonPropertyName("sections")]
            public List<Section> Sections { get; set; }
        }

        [JsonObject]
        public class Section
        {
            [JsonPropertyName("components")]
            public List<Component> Components { get; set; }
        }

        [JsonObject]
        public class Component
        {
            [JsonPropertyAttribute("ingredient")]
            public Ingredient IngredientData { get; set; }
        }

        [JsonObject]
        public class Ingredient
        {
            [JsonPropertyAttribute("name")]
            public string IngredientName { get; set; }
        }
    }
}