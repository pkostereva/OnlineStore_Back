using OnlineStoreBack.DB.Models;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public interface IOrderRepository
    {
        ValueTask<RequestResult<Order>> AddOrder(Order dataModel);
    }
}