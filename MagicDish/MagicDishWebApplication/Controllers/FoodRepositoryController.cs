using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagicDishWebApplication.Data;
using MagicDishWebApplication.Models;
using MagicDishWebApplication.Repository;
using AutoMapper;

namespace MagicDishWebApplication.Controllers
{
    public class FoodRepositoryController : Controller
    {
        private readonly MagicDishWebApplicationContext _context;
        private readonly IFoodRepositoryRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodRepositoryController(MagicDishWebApplicationContext context, IFoodRepositoryRepository foodRepository, IMapper mapper)
        {
            _context = context;
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        // GET: FoodRepositoryModels
        public async Task<IActionResult> Index()
        {
            var foodRepositories = _foodRepository.GetAll();
            var foodRepo = foodRepositories.Select(x => _mapper.Map<FoodRepositoryModel> (x ));  
            return foodRepo != null ? 
                          View(foodRepo) :
                          Problem("Entity set 'MagicDishWebApplicationContext.FoodRepositories'  is null.");
        }

        // GET: FoodRepositoryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodRepositories == null)
            {
                return NotFound();
            }

            var foodRepositoryModel = await _context.FoodRepositories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodRepositoryModel == null)
            {
                return NotFound();
            }

            return View(foodRepositoryModel);
        }

        // GET: FoodRepositoryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodRepositoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FoodRepositoryModel foodRepositoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodRepositoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodRepositoryModel);
            
        }

        // GET: FoodRepositoryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodRepositories == null)
            {
                return NotFound();
            }

            var foodRepositoryModel = await _context.FoodRepositories.FindAsync(id);
            if (foodRepositoryModel == null)
            {
                return NotFound();
            }
            return View(foodRepositoryModel);
        }

        // POST: FoodRepositoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodRepositoryModel foodRepositoryModel)
        {
            if (id != foodRepositoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodRepositoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodRepositoryModelExists(foodRepositoryModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodRepositoryModel);
        }

        // GET: FoodRepositoryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodRepositories == null)
            {
                return NotFound();
            }

            var foodRepositoryModel = await _context.FoodRepositories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodRepositoryModel == null)
            {
                return NotFound();
            }

            return View(foodRepositoryModel);
        }

        // POST: FoodRepositoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodRepositories == null)
            {
                return Problem("Entity set 'MagicDishWebApplicationContext.FoodRepositories'  is null.");
            }
            var foodRepositoryModel = await _context.FoodRepositories.FindAsync(id);
            if (foodRepositoryModel != null)
            {
                _context.FoodRepositories.Remove(foodRepositoryModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodRepositoryModelExists(int id)
        {
          return (_context.FoodRepositories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
