using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace MagicDish.Web.Controllers
{
    public class RecipesController
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<RecipeViewModel> model = new List<RecipeViewModel>();
            return View(model);
        }
    }
}
