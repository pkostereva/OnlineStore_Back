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
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetTotalWorthByCity();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<OrderWide>>> GetOrdersByTimeSpan(DateTime start, DateTime end)
        {
            var result = new RequestResult<List<OrderWide>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetOrdersByTimeSpan(start, end);
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<City>>> GetMostSoldProductInCities()
        {
            var result = new RequestResult<List<City>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetMostSoldProductInCities();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Product>>> GetFilteredProducts(ReportTypeEnum type)
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetFilteredProducts(type);
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<CategoryWithProducts>>> GetCategoriesWithFiveAndMoreProducts()
        {
            var result = new RequestResult<List<CategoryWithProducts>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetCategoriesWithFiveAndMoreProducts();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<TotalCostByCountry>> GetTotalCostByCountry()
        {
            var result = new RequestResult<TotalCostByCountry>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetTotalCostByCountry();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
