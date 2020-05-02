using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public interface IProductRepository
    {
        ValueTask<RequestResult<List<Product>>> GetAllProducts();
        ValueTask<RequestResult<List<Product>>> ProductSearch(ProductSearch dataModel);
    }
}