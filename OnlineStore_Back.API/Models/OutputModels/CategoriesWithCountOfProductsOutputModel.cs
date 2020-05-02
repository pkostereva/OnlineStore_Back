using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Models.OutputModels
{
    public class CategoriesWithCountOfProductsOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountOfProducts { get; set; }
    }
}
