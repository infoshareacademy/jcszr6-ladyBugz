using BusinessLogic;
using BusinessLogic.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicDishWebApplication.Controllers
{
    public class FoodRepoController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private IProductQuantityRepository _repository;

        public FoodRepoController(ILogger<HomeController> logger, IProductQuantityRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: FoodRepoController
        public async Task<IActionResult> Index()
        {
            List<ProductQuantity> products = await _repository.GetAsync();
            return View(products);
        }


        // GET: FoodRepoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FoodRepoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodRepoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: FoodRepoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FoodRepoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: FoodRepoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FoodRepoController/Delete/5
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
