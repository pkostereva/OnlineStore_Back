using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public interface IReportStorage
    {
        ValueTask<List<CityTotalWorth>> GetTotalWorthByCity();
        ValueTask<List<OrderWide>> GetOrdersByTimeSpan(DateTime start, DateTime end);
        ValueTask<List<City>> GetMostSoldProductInCities();
        ValueTask<List<CategoryWithProducts>> GetCategoriesWithFiveAndMoreProducts();
        ValueTask<List<Product>> GetFilteredProducts(ReportTypeEnum type);
        ValueTask<TotalCostByCountry> GetTotalCostByCountry();
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}