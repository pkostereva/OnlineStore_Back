using System.Collections.Generic;

namespace OnlineStoreBack.API.Models.InputModels
{
    public class OrderInputModel
    {
        public long? Id { get; set; }
        public int CityId { get; set; }
        public List<Order_ProductInputModel> ProductList { get; set; }
    }
}
