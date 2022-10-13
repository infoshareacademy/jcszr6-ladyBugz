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
                        f.Id,
                        p.Name,
                        p.Unit.UnitName,
                        p.ProductCategory.CategoryName,
                        p.IsVegan,
                        f.Amount,
                    })
                .Select(f => new FridgeContentViewModel
                {
                    Id = f.Id,
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
            model.ProductsDropdown = new SelectList(new List<string> { });
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

            var fridgeId = fridges.Where(u => u.UserId == userId).First().Id;
	
            var existingFrideProductId = GetFridgeProductIdByProductName(fridgeId, obj.ProductName);

			if (existingFrideProductId > 0)
            {
				TempData["Message"] = "Fridge product already exists. You have been redirected to Edit Page of existing product";
				return RedirectToAction("Edit", new { Id = existingFrideProductId });
			}

			fridgeProduct.Amount = obj.Amount;
			fridgeProduct.FridgeId = fridgeId;
			fridgeProduct.ProductId = availableProducts.Where(p => p.Name == obj.ProductName).First().Id;

			fridgeProducts.Add(fridgeProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private int GetFridgeProductIdByProductName(int fridgeId, string productName)
        {
            var results = from fridgeProduct in _context.FridgeProducts
                          join product in _context.AvailableProducts on fridgeProduct.ProductId equals product.Id
                          where (fridgeProduct.FridgeId == fridgeId) && (product.Name == productName)
                          select new { Id = fridgeProduct.Id };

            var fridgeProductResult = results.FirstOrDefault();

            return fridgeProductResult != null ? fridgeProductResult.Id : 0;
        }

        //GET
        public IActionResult Edit(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            var fridgeProduct = _context.FridgeProducts.Where(x => x.Id == Id).FirstOrDefault();

            if (fridgeProduct == null)
            {
                return NotFound();
            }

            FridgeProductViewModel fridgeProductView = new FridgeProductViewModel();

            var product = _context.AvailableProducts.Where(x => x.Id == fridgeProduct.ProductId).FirstOrDefault();
            var productCategory = _context.ProductCategories.Where(x => x.Id == product.ProductCategoryId).FirstOrDefault();
			var productUnit = _context.Units.Where(x => x.Id == product.UnitId).FirstOrDefault();

			fridgeProductView.Id = Id;
			fridgeProductView.ProductName = product.Name;
			fridgeProductView.ProductCategory = productCategory.CategoryName;
			fridgeProductView.Amount = fridgeProduct.Amount;
			fridgeProductView.Unit = productUnit.UnitName;

            return View(fridgeProductView);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FridgeProductViewModel obj)
        {
            var fridgeProduct = _context.FridgeProducts.Where(u => u.Id == obj.Id).FirstOrDefault();
            if (fridgeProduct == null)
            {
                return NotFound();
            }

            fridgeProduct.Amount = obj.Amount;

			_context.FridgeProducts.Update(fridgeProduct);
            _context.SaveChanges();
            TempData["success"] = "Amount updated successfully";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Delete(int Id)
        {
			if (Id == 0)
			{
				return NotFound();
			}

			var fridgeProduct = _context.FridgeProducts.Where(x => x.Id == Id).FirstOrDefault();

			if (fridgeProduct == null)
			{
				return NotFound();
			}

			FridgeProductViewModel fridgeProductView = new FridgeProductViewModel();

			var product = _context.AvailableProducts.Where(x => x.Id == fridgeProduct.ProductId).FirstOrDefault();
			var productCategory = _context.ProductCategories.Where(x => x.Id == product.ProductCategoryId).FirstOrDefault();
			var productUnit = _context.Units.Where(x => x.Id == product.UnitId).FirstOrDefault();

			fridgeProductView.Id = Id;
			fridgeProductView.ProductName = product.Name;
			fridgeProductView.ProductCategory = productCategory.CategoryName;
			fridgeProductView.Amount = fridgeProduct.Amount;
			fridgeProductView.Unit = productUnit.UnitName;

			return View(fridgeProductView);
		}

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FridgeProductViewModel obj)
        {
			var fridgeProduct = _context.FridgeProducts.Where(u => u.Id == obj.Id).FirstOrDefault();

			if (fridgeProduct == null)
            {
                return NotFound();
            }

			_context.FridgeProducts.Remove(fridgeProduct);
            _context.SaveChanges();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


