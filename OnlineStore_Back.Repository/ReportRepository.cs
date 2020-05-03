using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.DB.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public class ReportRepository : IReportRepository
    {
        private IReportStorage _reportStorage;
        public ReportRepository(IReportStorage reportStorage)
        {
            _reportStorage = reportStorage;
        }

        public async ValueTask<RequestResult<List<CityTotalWorth>>> GetTotalWorthByCity()
        {
            var result = new RequestResult<List<CityTotalWorth>>();
            try
            {
                result.RequestData = await _reportStorage.GetTotalWorthByCity();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<OrderByTimeSpan>>> GetOrdersByTimeSpan(DateTime start, DateTime end)
        {
            var result = new RequestResult<List<OrderByTimeSpan>>();
            try
            {
                result.RequestData = await _reportStorage.GetOrdersByTimeSpan(start, end);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<City>>> GetMostSoldProductInCities()
        {
            var result = new RequestResult<List<City>>();
            try
            {
                result.RequestData = await _reportStorage.GetMostSoldProductInCities();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Product>>> GetFilteredProducts(ReportTypeEnum type)
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                result.RequestData = await _reportStorage.GetFilteredProducts(type);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<CategoryWithProducts>>> GetCategoriesWithFiveAndMoreProducts()
        {
            var result = new RequestResult<List<CategoryWithProducts>>();
            try
            {
                result.RequestData = await _reportStorage.GetCategoriesWithFiveAndMoreProducts();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<TotalCostByCountry>> GetTotalCostByCountry()
        {
            var result = new RequestResult<TotalCostByCountry>();
            try
            {
                result.RequestData = await _reportStorage.GetTotalCostByCountry();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
