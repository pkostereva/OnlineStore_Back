using System;

namespace OnlineStoreBack.DB.Models
{
    public class OrderByTimeSpan
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public City City { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
