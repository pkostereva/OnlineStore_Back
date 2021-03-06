﻿using OnlineStoreBack.DB.Storages;
using OnlineStoreBack.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStoreBack.Repository;

namespace OnlineStoreBack.Repository
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

        public async ValueTask<RequestResult<List<Product>>> ProductSearch(ProductSearch dataModel)
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                result.RequestData = await _productStorage.ProductSearch(dataModel);
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
