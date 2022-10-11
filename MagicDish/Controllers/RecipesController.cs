using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MagicDish.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Recipe> recipes = _context.Recipes.ToList();
            return View(recipes);
        }
    }
}
