
namespace OnlineStoreBack.DB.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
