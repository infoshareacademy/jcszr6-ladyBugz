using BusinessLogic;

namespace MagicDishWebApplication.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
    }
}