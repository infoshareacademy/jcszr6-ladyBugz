using System.Security.Claims;
using System.Text.Json.Serialization;
using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            var result = allRecipes.GroupBy(x => x.Id).OrderByDescending(y => y.Count()).ToList();

            //help nie potrafie zmapowac List<DtoRecipe> na List<Recipe>, zeby podac jako model do widoku  
            //(ingrideents musza pozostac null na razie, bo to inna kwestia, zeby je zmatchowac z lodowka)
            //probowalam w ten sposob:
            //List<Recipe> recipesToDisplay = result.Select(r => new Recipe() { Id = r.Select(i => i.Id.), Name = r.Select(i => i.Name)

            return View();
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

        public class ApiResponse 
        {
			[JsonPropertyName("count")]
			public int Count { get; set; }

			[JsonPropertyName("results")]
			public List<RecipeDto> Results { get; set; }
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

                var result = JsonConvert.DeserializeObject<ApiResponse>(body);

                return result.Results;
            }

            return null;
        }

        public IActionResult Index2()
        {
            List<Recipe> recipes = _context.Recipes.Include(r => r.Ingredients).ToList();
            return View(recipes);
        }
    }
}
