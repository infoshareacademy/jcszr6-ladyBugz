using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagicDishWebApplication.Data;
using MagicDishWebApplication.Models;
using MagicDishWebApplication.Repository;
using AutoMapper;

namespace MagicDishWebApplication.Controllers
{
    public class RecipeController : Controller
    {
        private readonly MagicDishWebApplicationContext _context;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(MagicDishWebApplicationContext context, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _context = context;
            _recipeRepository = recipeRepository;
            _mapper = mapper;

        }

        // GET: RecipesModels
        public async Task<IActionResult> Index()
        {
            var recipes = _recipeRepository.GetAll();
            var recipe = recipes.Select(x => _mapper.Map<RecipeModel>(x));
            return recipe != null ? 
                          View(recipe) :
                          Problem("Entity set 'MagicDishWebApplicationContext.Recipes'  is null.");
        }

        // GET: RecipesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipesModel = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipesModel == null)
            {
                return NotFound();
            }

            return View(recipesModel);
        }

        // GET: RecipesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipesModels/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CookingTimeInMinutes,IsVegeterian,Description")] RecipeModel recipesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipesModel);
        }

        // GET: RecipesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipesModel = await _context.Recipes.FindAsync(id);
            if (recipesModel == null)
            {
                return NotFound();
            }
            return View(recipesModel);
        }

        // POST: RecipesModels/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CookingTimeInMinutes,IsVegeterian,Description")] RecipeModel recipesModel)
        {
            if (id != recipesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipesModelExists(recipesModel.Id))
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
            return View(recipesModel);
        }

        // GET: RecipesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipesModel = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipesModel == null)
            {
                return NotFound();
            }

            return View(recipesModel);
        }

        // POST: RecipesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'MagicDishWebApplicationContext.Recipes'  is null.");
            }
            var recipesModel = await _context.Recipes.FindAsync(id);
            if (recipesModel != null)
            {
                _context.Recipes.Remove(recipesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipesModelExists(int id)
        {
          return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
