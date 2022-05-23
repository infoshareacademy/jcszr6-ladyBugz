namespace BusinessLogic
{
    public class ProductService
    {
        private static List<Product> Products = new List<Product>
            {
                new Product(1, "milk", UnitOfMeasure.mililiters),
                new Product(2, "wheat", UnitOfMeasure.grams),
                new Product(3, "egg", UnitOfMeasure.pieces),
                new Product(4, "sugar", UnitOfMeasure.grams)
            };

        //tutaj bedzie pobieranie z pliku tekstowego
        public static List<Product> GetProducts()
        {
            return Products;
        }
    }
}
