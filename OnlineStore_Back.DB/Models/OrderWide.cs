using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreBack.DB.Models
{
    public class OrderWide : Order
    {
        public Product Product { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
    }
}
