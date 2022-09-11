using MagicDishWebApplication;
using MagicDishWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace MagicDishWebApplication.Repository
{
    public class FoodRepositoryRepository : IFoodRepositoryRepository
    {
        private MagicDishWebApplicationContext _context;
        public FoodRepositoryRepository(MagicDishWebApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodRepository> GetAll()
        {
             return (IEnumerable<FoodRepository>)_context.FoodRepositories.AsEnumerable();
        }
    }
}
