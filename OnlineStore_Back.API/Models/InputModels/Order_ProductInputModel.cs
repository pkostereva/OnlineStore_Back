namespace OnlineStoreBack.API.Models.InputModels
{
    public class Order_ProductInputModel
    {
        public long? Id { get; set; }
        //public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public int LocalPrice { get; set; }
    }
}