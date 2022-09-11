using BusinessLogic;

namespace MagicDishWebApplication.Repository
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAsync();
    }
}