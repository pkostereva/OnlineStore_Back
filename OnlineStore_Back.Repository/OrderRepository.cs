using Newtonsoft.Json.Linq;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.DB.Storages;
using OnlineStoreBack.Repository.Common;
using System;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderStorage _orderStorage;

        public OrderRepository(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }

        public async ValueTask<RequestResult<Order>> AddOrder(Order dataModel)
        {
            var result = new RequestResult<Order>();
            try
            {
                string path;
                decimal curCurrency = 1;
                switch (dataModel.City.Id)
                {
                    case (int?)CountryEnum.Ukraine:
                        path = "UAH";
                        curCurrency = await CurrentCurrency.GetCurrency(path);
                        break;
                    case (int?)CountryEnum.Belarus:
                        path = "BYN";
                        curCurrency = await CurrentCurrency.GetCurrency(path);
                        break;
                }
                _orderStorage.TransactionStart();
                result.RequestData = await _orderStorage.AddOrder(dataModel, curCurrency);
                _orderStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _orderStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
