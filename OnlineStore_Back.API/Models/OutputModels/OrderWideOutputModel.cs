using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Models.OutputModels
{
    public class OrderWideOutputModel
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SubCategory { get; set; }
    }
}
