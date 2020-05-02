using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public interface IProductStorage
    {
        ValueTask<List<Product>> ProductsGetAll();
        ValueTask<List<Product>> ProductSearch(ProductSearch dataModel);

        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}