using BusinessLogic;

namespace MagicDishWebApplication.Repository
{
    public interface IProductQuantityRepository
    {
        Task<List<ProductQuantity>> GetAsync();
    }
}