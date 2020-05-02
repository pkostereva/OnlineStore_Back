using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Models.OutputModels
{
    public class MostSoldProductInCityOutputModel
    {
        public string CityName { get; set; }
        public string Product { get; set; }
    }
}
