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
                    ProductName = f.Name,
                    Amount = f.Amount,
                    UnitOfMeasure = f.UnitOfMeasure,
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

            var productCategories = Enum.GetNames(typeof(ProductCategory)).ToList();
            var products = _context.AvailableProducts.Select(p => p.Name).ToList();
            var unitsOfMeasure = Enum.GetNames(typeof(UnitOfMeasure)).ToList();

            model.ProductCategoriesDropdown = new SelectList(productCategories);
            model.ProductsDropdown = new SelectList(new List<string> {});
            model.UnitsOfMeasureDropdown = new SelectList(unitsOfMeasure);

            return View(model);
        }

        [HttpPost]
        public JsonResult SetDropdownList(string value)
        {
            var availableProducts = _context.AvailableProducts;
            var enumValue = (ProductCategory)Enum.Parse(typeof(ProductCategory), value);
            var productsList = availableProducts.Where(p => p.ProductCategory == enumValue).Select(p => p.Name).ToList();

            return Json(productsList);
        }

        [HttpPost]
        public JsonResult SetUnitOfMeasure(string value)
        {
            var availableProducts = _context.AvailableProducts;
            var unitOfMeasure = availableProducts.Where(p => p.Name == value).Select(p => p.UnitOfMeasure).FirstOrDefault().ToString();
            return Json(unitOfMeasure);
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
