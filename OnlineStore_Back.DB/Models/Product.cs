
namespace OnlineStore_Back.DB.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
