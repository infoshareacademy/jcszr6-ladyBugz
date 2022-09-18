using BusinessLogic;
using MagicDishWebApplication;

namespace MagicDishWebApplication.Repository
{
    public interface IFoodRepositoryRepository
    {
        IEnumerable<FoodRepository> GetAll();
    }
}
