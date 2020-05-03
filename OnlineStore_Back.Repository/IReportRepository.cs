using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public interface IReportRepository
    {
        ValueTask<RequestResult<List<CityTotalWorth>>> GetTotalWorthByCity();
        ValueTask<RequestResult<List<OrderByTimeSpan>>> GetOrdersByTimeSpan(DateTime start, DateTime end);
        ValueTask<RequestResult<List<City>>> GetMostSoldProductInCities();
        ValueTask<RequestResult<List<Product>>> GetFilteredProducts(ReportTypeEnum type);
        ValueTask<RequestResult<List<CategoryWithProducts>>> GetCategoriesWithFiveAndMoreProducts();
        ValueTask<RequestResult<TotalCostByCountry>> GetTotalCostByCountry();
    }
}