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
            var availableProducts = _context.AvailableProducts;
            var fridgeProducts = _context.FridgeProducts;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<FridgeContentViewModel> fridgeContentList = fridgeProducts.Where(u => u.Fridge.UserId == userId)
                .Join(availableProducts,
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
                    ProductCategory = f.ProductCategory,
                    Name = f.Name,
                    Amount = f.Amount,
                    UnitOfMeasure = f.UnitOfMeasure,
                    IsVegan = f.IsVegan,
                })
                .OrderBy(p => p.Name)
                .ToList();

            return View(fridgeContentList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FridgeContentViewModel obj)
        {
            var availableProducts = _context.AvailableProducts;
            var fridgeProducts = _context.FridgeProducts;
            var fridges = _context.Fridges;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            FridgeProduct fridgeProduct = new FridgeProduct();
            fridgeProduct.Amount = obj.Amount;
            fridgeProduct.FridgeId = fridges.Where(u => u.UserId == userId).FirstOrDefault().Id;
            fridgeProduct.ProductId = availableProducts.Where(p => p.Name == obj.Name).FirstOrDefault().Id;

            fridgeProducts.Add(fridgeProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
