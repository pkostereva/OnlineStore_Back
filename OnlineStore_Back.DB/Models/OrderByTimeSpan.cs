using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreBack.DB.Models
{
    public class OrderByTimeSpan
    {
        public Order Order { get; set; }
        public City City { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
