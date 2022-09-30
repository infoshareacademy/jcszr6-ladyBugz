using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
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
            var fridgeProductList = fridgeContent.Where(u => u.Fridge.UserId == userId)
                .Join(productsInformation,
                    f => f.ProductId,
                    p => p.Id, (f, p) => new
                    {
                        p.Name,
                        p.UnitOfMeasure,
                        p.ProductCategory,
                        p.isVegan,
                        f.Amount,
                    })
                .OrderBy(p => p.Name);

            return View();
        }
    }
}
