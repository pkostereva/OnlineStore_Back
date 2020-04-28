using Dapper;
using Microsoft.Extensions.Options;
using OnlineStoreBack.API.Configuration;
using OnlineStoreBack.DB.Models;
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

        public void TransactionStart()
        {
            if (connection == null) { connection = new SqlConnection("Data Source=185.26.112.224;Initial Catalog=CrmDb_Restored;User ID=dev;Password=qwe!23;"); }
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

        internal static class SpName
        {
            public const string ProductsGetAll = "Products_SelectAll";
            public const string CityGetById = "City_SelectById";
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
                    //transaction: transaction,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<City> CityGetById(long id)
        {
            try
            {
                var result = await connection.QueryAsync<City>(
                    SpName.CityGetById,
                    param: new { id },
                    //transaction: transaction,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
