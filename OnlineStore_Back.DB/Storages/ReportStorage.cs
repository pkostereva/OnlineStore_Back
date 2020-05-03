using Dapper;
using Microsoft.Extensions.Options;
using OnlineStoreBack.API.Configuration;
using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System;
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

        public ReportStorage(IOptions<ConfigurationOptions> configurationOptions)
        {
            this.connection = new SqlConnection(configurationOptions.Value.DBConnectionString);
        }

        internal static class SpName
        {
            public const string TotalWorthByCity = "GetTotalWorthByCity";
            public const string OrdersByTimeSpan = "GetOrdersByTimeSpan";
            public const string MostSoldProductInCities = "GetMostSoldProductInCities";
            public const string ProductsInStockButNotInCities = "GetProductsInStockButNotInCities";
            public const string ProductsNeverOrdered = "GetProductsNeverOrdered";
            public const string ProductsOrderedButNotInCities = "GetProductsOrderedButNotInCities";
            public const string CategoriesWithFiveProducts = "GetCategoriesWithFiveAndMoreProducts";
            public const string TotalCostByCountry = "GetTotalIncomeRuAndForeign";
        }

        public async ValueTask<List<CityTotalWorth>> GetTotalWorthByCity()
        {
            try
            {
                var result = await connection.QueryAsync<City, decimal, CityTotalWorth>(
                    SpName.TotalWorthByCity, (c, m) =>
                    {
                        CityTotalWorth totalWorth = new CityTotalWorth();
                        totalWorth.City = c;
                        totalWorth.Worth = m;
                        return totalWorth;
                    },
                    param: null,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Money");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<OrderByTimeSpan>> GetOrdersByTimeSpan(DateTime start, DateTime end)
        {
            try
            {
                var result = await connection.QueryAsync<OrderByTimeSpan, City, int, decimal, Product, Category, OrderByTimeSpan>(
                    SpName.OrdersByTimeSpan,
                    (o, c, tq, tc, p, cat) =>
                    {
                        OrderByTimeSpan order = o;
                        o.City = c;
                        o.TotalQuantity = tq;
                        o.TotalCost = tc;
                        o.Product = p;
                        o.Category = cat;
                        return order;
                    },
                    param: new { start, end },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id, TotalQuantity, TotalCost, Id, Id");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<City>> GetMostSoldProductInCities()
        {
            try
            {
                var result = await connection.QueryAsync<City, string, City>(
                    SpName.MostSoldProductInCities, (c, p) =>
                    {
                        City city = c;
                        city.Product = p;
                        return city;
                    },
                    param: null,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Product");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<Product>> GetFilteredProducts(ReportTypeEnum type)
        {
            string procName = SpName.ProductsNeverOrdered;
            switch (type)
            {
                case ReportTypeEnum.ProductsNeverOrdered:
                    procName = SpName.ProductsNeverOrdered;
                    break;
                case ReportTypeEnum.ProductsInStockButNotInCities:
                    procName = SpName.ProductsInStockButNotInCities;
                    break;
                case ReportTypeEnum.ProductsOrderedButNotInCities:
                    procName = SpName.ProductsOrderedButNotInCities;
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
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async ValueTask<List<CategoryWithProducts>> GetCategoriesWithFiveAndMoreProducts()
        {
            try
            {
                var result = await connection.QueryAsync<Category, int, CategoryWithProducts>(
                    SpName.CategoriesWithFiveProducts, (c, count) =>
                    {
                        CategoryWithProducts category = new CategoryWithProducts();
                        category.Category = c;
                        category.CountOfProducts = count;
                        return category;
                    },
                    param: null,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "CountOfProducts");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<TotalCostByCountry> GetTotalCostByCountry()
        {
            try
            {
                var result = await connection.QueryAsync<TotalCostByCountry>(
                    SpName.TotalCostByCountry,
                    param: null,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
