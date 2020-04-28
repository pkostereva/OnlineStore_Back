using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public interface IProductStorage
    {
        ValueTask<List<Product>> ProductsGetAll();

        ValueTask<City> CityGetById(long id);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}