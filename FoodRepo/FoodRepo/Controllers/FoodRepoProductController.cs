using FoodRepo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodRepo.Controllers
{
    public class FoodRepoProductController : Controller
    {
        static List<FoodRepoProduct> foodRepoProducts = new List<FoodRepoProduct>
        {
            new FoodRepoProduct
            {
                Id = 1,
                Name = "pasta",
                Quantity = 100,
                UnitOfMeasure = UnitOfMeasure.grams
            },
            new FoodRepoProduct
            {
                Id = 2,
                Name = "tomato",
                Quantity = 3,
                UnitOfMeasure = UnitOfMeasure.pieces
            },
            new FoodRepoProduct
            {
                Id = 3,
                Name = "salt",
                Quantity = 100,
                UnitOfMeasure = UnitOfMeasure.grams
            },
            new FoodRepoProduct
            {
                Id = 4,
                Name = "passata",
                Quantity = 1000,
                UnitOfMeasure = UnitOfMeasure.mililiters
            },
        };

        // GET: FoodRepoProductController
        public ActionResult Index()
        {
            return View(foodRepoProducts);
        }

        // GET: FoodRepoProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(foodRepoProducts.FirstOrDefault(p => p.Id == id));
        }

        // GET: FoodRepoProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodRepoProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodRepoProduct product)
        {
            try
            {
                foodRepoProducts.Add(product);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: FoodRepoProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(foodRepoProducts.FirstOrDefault(p => p.Id == id));
        }

  

        // GET: FoodRepoProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FoodRepoProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
