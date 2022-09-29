using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MagicDish.Web.Controllers
{
    public class FridgeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FridgeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Fridge> fridgeList = _context.Fridges.ToList();
            //need to return the fridge for a given user
            Fridge fridge = fridgeList.FirstOrDefault();
            IList<ProductQuantity> fridgeProducts = fridge.Products;
            return View(fridgeProducts);
        }
    }
}
