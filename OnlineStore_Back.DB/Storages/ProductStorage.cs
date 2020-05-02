using Dapper;
using Microsoft.Extensions.Options;
using OnlineStoreBack.API.Configuration;
using OnlineStoreBack.DB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public class ProductStorage : IProductStorage
    {
        private IDbConnection connection;

        private IDbTransaction transaction;

        public ProductStorage(IOptions<ConfigurationOptions> configurationOptions)
        {
            this.connection = new SqlConnection(configurationOptions.Value.DBConnectionString);
        }

        internal static class SpName
        {
            public const string ProductsGetAll = "Products_SelectAll";
            public const string ProductsSearch = "Product_Search";
        }

        public void TransactionStart()
        {
            if (connection == null) { connection = new SqlConnection("Data Source=(local);Initial Catalog=SQL_HW_Kostereva;Integrated Security = True;"); }
            connection.Open();
            transaction = this.connection.BeginTransaction();
        }

        public void TransactionCommit()
        {
            this.transaction?.Commit();
            connection?.Close();
        }

        public void TransactioRollBack()
        {
            this.transaction?.Rollback();
            connection?.Close();
        }

        public async ValueTask<List<Product>> ProductsGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<Product, Category, Product>(
                    SpName.ProductsGetAll,
                    (product, category) =>
                    {
                        Product newProduct = product;
                        newProduct.Category = category;
                        return newProduct;
                    },
                    param: null,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<Product>> ProductSearch(ProductSearch dataModel)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(new
                {
                    dataModel.Id,
                    dataModel.Brand,
                    dataModel.Model,
                    dataModel.Price,
                    dataModel.CategoryId,
                    dataModel.SubCategoryId
                });
                var result = await connection.QueryAsync<Product, Category, Product>(
                    SpName.ProductsSearch,
                    (p, c) =>
                    {
                        Product product = p;
                        product.Category = c;
                        return product;
                    },
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
