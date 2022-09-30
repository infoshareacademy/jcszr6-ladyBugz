using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MagicDish.Web.Controllers
{
    public class FridgeContentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FridgeContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productsInformation = _context.AvailableProducts;
            var fridgeContent = _context.FridgeProducts;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<FridgeContentViewModel> fridgeContentList = fridgeContent.Where(u => u.Fridge.UserId == userId)
                .Join(productsInformation,
                    f => f.ProductId,
                    p => p.Id, (f, p) => new
                    {
                        p.Name,
                        p.UnitOfMeasure,
                        p.ProductCategory,
                        p.IsVegan,
                        f.Amount,
                    })
                .Select(f => new FridgeContentViewModel
                {
                    Name = f.Name,
                    UnitOfMeasure = f.UnitOfMeasure,
                    ProductCategory = f.ProductCategory,
                    IsVegan = f.IsVegan,
                    Amount = f.Amount,
                })
                .OrderBy(p => p.Name)
                .ToList();

            return View(fridgeContentList);
        }
    }
}
