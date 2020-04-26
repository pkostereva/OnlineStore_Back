using OnlineStore_Back.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_Back.DB.Storages
{
    public interface IProductStorage
    {
        ValueTask<List<Product>> ProductsGetAll();
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}