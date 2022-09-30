using System.ComponentModel.DataAnnotations;

namespace MagicDish.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public bool IsVegan { get; set; }
    }
}