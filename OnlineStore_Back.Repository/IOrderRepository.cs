using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public interface IOrderRepository
    {
        ValueTask<RequestResult<Order>> AddOrder(Order dataModel);
    }
}