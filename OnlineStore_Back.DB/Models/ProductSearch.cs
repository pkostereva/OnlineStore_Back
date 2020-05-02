using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreBack.DB.Models
{
    public class ProductSearch
    {
        public long? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal? Price { get; set; }
    }
}
