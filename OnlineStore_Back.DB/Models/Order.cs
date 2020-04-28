using System;
using System.Collections.Generic;

namespace OnlineStoreBack.DB.Models
{
    public class Order
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public City City { get; set; }
        public List<Order_Product> OrderDetails {get; set;}
    }
}
