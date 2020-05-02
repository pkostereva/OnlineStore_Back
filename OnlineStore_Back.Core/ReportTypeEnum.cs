using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreBack.Core
{
    public enum ReportTypeEnum
    {
        ProductsNeverOrdered = 1,
        ProductsInStockButNotInCities,
        ProductsOrderedButNotInCities
    }
}
