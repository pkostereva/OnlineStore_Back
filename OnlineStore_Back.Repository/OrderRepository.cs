using OnlineStoreBack.DB.Models;
using OnlineStoreBack.DB.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderStorage _orderStorage;

        public OrderRepository(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }

        public async ValueTask<RequestResult<Order>> AddOrder(Order dataModel)
        {
            var result = new RequestResult<Order>();
            try
            {
                //_orderStorage.TransactionStart();
                result.RequestData = await _orderStorage.AddOrder(dataModel);
                //_leadStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                //_orderStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
