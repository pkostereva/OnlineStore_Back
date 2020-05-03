
namespace OnlineStoreBack.API.Models.InputModels
{
    public class ProductSearchInputModel
    {
        public long? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Price { get; set; }
    }
}
