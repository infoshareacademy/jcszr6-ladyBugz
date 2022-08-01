using BusinessLogic;

namespace MagicDishWebApplication.Models
{
    public class FoodRepositoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductQuantity> Products { get; set; }
    }
}
