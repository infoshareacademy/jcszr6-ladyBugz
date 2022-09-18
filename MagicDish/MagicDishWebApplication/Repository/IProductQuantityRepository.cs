using BusinessLogic;

namespace MagicDishWebApplication.Repository
{
    public interface IProductQuantityRepository
    {
        IEnumerable<ProductQuantity> GetAll();
    }
}