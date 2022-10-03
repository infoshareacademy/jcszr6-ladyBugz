using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var users = _context.Users;


            List<FridgeContentViewModel> fridgeContentList = fridgeProducts.Where(u => u.Fridge.UserId == userId)
                .Join(availableProducts,
                    f => f.ProductId,
                    p => p.Id, (f, p) => new
                    {
                        p.Name,
                        p.Unit.UnitName,
                        p.ProductCategory.CategoryName,
                        p.IsVegan,
                        f.Amount,
                    })
                .Select(f => new FridgeContentViewModel
                {
                    ProductCategory = f.CategoryName,
                    ProductName = f.Name,
                    Amount = f.Amount,
                    Unit = f.UnitName,
                    IsVegan = f.IsVegan,
                })
                .OrderBy(p => p.ProductName)
                .ToList();

            return View(fridgeContentList);
        }

        //GET
        public IActionResult Create()
        {
            FridgeContentViewModel model = new FridgeContentViewModel();

            var productCategories = _context.ProductCategories.Select(c => c.CategoryName).ToList();
            var products = _context.AvailableProducts.Select(p => p.Name).ToList();
            var units = _context.Units.Select(u => u.UnitName).ToList();

            model.ProductCategoriesDropdown = new SelectList(productCategories);
            model.ProductsDropdown = new SelectList(new List<string> {});
            model.UnitsOfMeasureDropdown = new SelectList(units);

            return View(model);
        }

        [HttpPost]
        public JsonResult SetDropdownList(string value)
        {
            var availableProducts = _context.AvailableProducts;
            var units = _context.Units;

            var productsList = availableProducts.Where(p => p.ProductCategory.CategoryName == value).Select(p => p.Name).ToList();

            //var enumValue = (ProductCategory)Enum.Parse(typeof(ProductCategory), value);
            //var productsList = availableProducts.Where(p => p.ProductCategory == enumValue).Select(p => p.Name).ToList();

            return Json(productsList);
        }

        [HttpPost]
        public JsonResult SetUnit(string value)
        {
            var availableProducts = _context.AvailableProducts;

            var unit = availableProducts.Where(p => p.Name == value).Select(p => p.Unit.UnitName).FirstOrDefault().ToString();
            //var unit = availableProducts.Where(p => p.Name == value).Select(p => p.Unit).FirstOrDefault().ToString();
            return Json(unit);
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
            fridgeProduct.ProductId = availableProducts.Where(p => p.Name == obj.ProductName).FirstOrDefault().Id;

            fridgeProducts.Add(fridgeProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
