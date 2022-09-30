using MagicDish.Core.Models;
using MagicDish.Persistance.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MagicDish.Web.Controllers
{
    public class FridgeContentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FridgeContentController(ApplicationDbContext context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
