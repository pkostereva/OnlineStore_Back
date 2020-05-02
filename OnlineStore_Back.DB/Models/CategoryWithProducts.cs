using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreBack.DB.Models
{
    public class CategoryWithProducts
    {   
        public Category Category { get; set; }
        public int CountOfProducts { get; set; }
    }
}
