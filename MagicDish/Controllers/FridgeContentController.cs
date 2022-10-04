using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using MagicDish.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            return Json(productsList);
        }

        [HttpPost]
        public JsonResult SetUnit(string value)
        {
            var availableProducts = _context.AvailableProducts;
            var unit = availableProducts.Where(p => p.Name == value).Select(p => p.Unit.UnitName).FirstOrDefault().ToString();
            return Json(unit);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FridgeContentViewModel obj)
        {
            FridgeProduct fridgeProduct = new FridgeProduct();

            var availableProducts = _context.AvailableProducts;
            var fridgeProducts = _context.FridgeProducts;
            var fridges = _context.Fridges;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            fridgeProduct.Amount = obj.Amount;
            fridgeProduct.FridgeId = fridges.Where(u => u.UserId == userId).First().Id;
            fridgeProduct.ProductId = availableProducts.Where(p => p.Name == obj.ProductName).First().Id;

            fridgeProducts.Add(fridgeProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(string? productName)
        {
            if (productName == null)
            {
                return NotFound();
;           }

            var productFromDb = _context.FridgeProducts.FirstOrDefault(p => p.Product.Name == productName);

            if (productFromDb == null)
            {
                return NotFound();
            }

            FridgeProductViewModel product = new FridgeProductViewModel();
            var availableProducts = _context.AvailableProducts;

            product.ProductName = productName;
            product.ProductCategory = availableProducts.Where(c => c.Name == productName).Select(c => c.ProductCategory.CategoryName).FirstOrDefault();
            product.Amount = productFromDb.Amount;
            product.Unit = availableProducts.Where(c => c.Name == productName).Select(c => c.Unit.UnitName).FirstOrDefault();


            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FridgeProductViewModel obj)
        {
            var availableProducts = _context.AvailableProducts;
            var fridgeProducts = _context.FridgeProducts;
            var fridges = _context.Fridges;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var fridgeProduct = fridgeProducts.Where(p => p.Product.Name == obj.ProductName).First();
            fridgeProduct.Amount = obj.Amount;


            fridgeProducts.Update(fridgeProduct);
            await _context.SaveChangesAsync();
            TempData["success"] = "Amount updated successfully";
            return RedirectToAction("Index");
        }
    }
}


