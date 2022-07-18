using BusinessLogic;
using BusinessLogic.Repository;
using MagicDishWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace MagicDishWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository _repository;

        public HomeController(ILogger<HomeController> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _repository.GetAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}