using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public interface IOrderStorage
    {
        ValueTask<Order> AddOrder(Order model);
        ValueTask AddOrderDetails(List<Order_Product> orderDetails, long? orderId);
        ValueTask<Order> GetOrderWithDetailsByOrderId(long? id);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}