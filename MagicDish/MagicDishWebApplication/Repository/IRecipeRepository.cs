using BusinessLogic;

namespace MagicDishWebApplication.Repository
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAll();
    }
}