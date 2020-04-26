using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Back.API.Models.OutputModels
{
    public class ProductOutputModel
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
