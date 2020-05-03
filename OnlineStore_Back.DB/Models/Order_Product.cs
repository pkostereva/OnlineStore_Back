
namespace OnlineStoreBack.DB.Models
{
    public class Order_Product
    {
        public long? Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal LocalPrice { get; set; }
    }
}