namespace BusinessLogic
{
    public class ProductQuantity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public ProductQuantity()
        {
        }
        
        public ProductQuantity(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
