
using System.Collections.Generic;

namespace OnlineStoreBack.API.Models.OutputModels
{
    public class OrderOutputModel
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public string CityName { get; set; }
        public List<Order_ProductOutputModel> OrderDetails { get; set; }

    }
}
