using OnlineStore_Back.DB.Models;
using OnlineStore_Back.DB.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_Back.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IProductStorage _productStorage;

        public ProductRepository(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public async ValueTask<RequestResult<List<Product>>> GetAllProducts()
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                result.RequestData = await _productStorage.ProductsGetAll();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
