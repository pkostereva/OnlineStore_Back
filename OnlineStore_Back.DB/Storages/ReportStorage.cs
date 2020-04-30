using Dapper;
using Microsoft.Extensions.Options;
using OnlineStoreBack.API.Configuration;
using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public class ReportStorage : IReportStorage
    {
        private IDbConnection connection;

        private IDbTransaction transaction;

        public ReportStorage(IOptions<ConfigurationOptions> configurationOptions)
        {
            this.connection = new SqlConnection(configurationOptions.Value.DBConnectionString);
        }

        internal static class SpName
        {
            public const string NobodyBought = "NobodyBuy";
            public const string InStockNotInCities = "IsInStorageIsNotInCities";
            public const string OrderedButNotInCities = "OrderedButNotInCities";
        }

        public async ValueTask<List<Product>> GetProduct(ReportTypeEnum type)
        {
            string procName = SpName.NobodyBought;
            switch (type)
            {
                case ReportTypeEnum.NobodyBought:
                    procName = SpName.NobodyBought;
                    break;
                case ReportTypeEnum.InStockNotInCities:
                    procName = SpName.InStockNotInCities;
                    break;
                case ReportTypeEnum.OrderedNotInCity:
                    procName = SpName.OrderedButNotInCities;
                    break;
            }
            try
            {
                var result = await connection.QueryAsync<Product, Category, Product>(
                    procName,
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
    }
}
