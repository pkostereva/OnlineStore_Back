
namespace OnlineStoreBack.API.Models.OutputModels
{
    public class Order_ProductOutputModel
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal Localprice { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SubCategory { get; set; }
    }
}