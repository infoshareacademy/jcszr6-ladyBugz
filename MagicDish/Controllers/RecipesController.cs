using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MagicDish.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }


        //    public IActionResult Index()
        //    {
        //        //get all ingredients
        //        var ingredients = _context.Ingredients;
        //        //get user id
        //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        //get all the products
        //        var products = _context.AvailableProducts;
        //        //get fridge products for a given user
        //        var fridgeProducts = _context.FridgeProducts.Where(u => u.Fridge.UserId == userId).ToList();
        //        //get ingredients that exist in fridge and recipes
        //        List<Ingredient> ingredientsFromFridge = ingredients
        //            .Join(fridgeProducts,
        //            i => i.ProductId,
        //            f => f.ProductId, (i, f) => new
        //            {
        //                i.Id,
        //                i.Quantity,
        //                i.RecipeId,
        //                i.ProductId
        //            })
    


        //    return View(recipes);
        //    }


        //}
    
    public IActionResult Index()
        {
            List<Recipe> recipes = _context.Recipes.Include(r => r.Ingredients).ToList();
            return View(recipes);
        }
    }
}
