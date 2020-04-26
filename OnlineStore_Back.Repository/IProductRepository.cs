using OnlineStore_Back.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_Back.Repository
{
    public interface IProductRepository
    {
        ValueTask<RequestResult<List<Product>>> GetAllProducts();
    }
}