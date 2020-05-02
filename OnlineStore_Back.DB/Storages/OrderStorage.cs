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
    public class OrderStorage : IOrderStorage
    {
        private IDbConnection connection;

        private IDbTransaction transaction;

        public OrderStorage(IOptions<ConfigurationOptions> configurationOptions)
        {
            this.connection = new SqlConnection(configurationOptions.Value.DBConnectionString);
        }

        internal static class SpName
        {
            public const string OrderAdd = "Order_Insert";
            public const string OrderDetailsAdd = "Order_Product_Insert";
            public const string OrderWithDetailsGetById = "GetOrderWithDetailsByOrderId";
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

        public async ValueTask<Order> AddOrder(Order model, decimal rate)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters(new
                    {
                        cityId = model.City.Id
                    });
                var orderId = await connection.QueryAsync<long>(
                    SpName.OrderAdd,
                    parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                model.Id = orderId.FirstOrDefault();

                await AddOrderDetails(model.OrderDetails, model.Id, rate);
                return await GetOrderWithDetailsByOrderId(model.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask AddOrderDetails(List<Order_Product> orderDetails, long? orderId, decimal rate)
        {
            try
            {
                foreach (Order_Product item in orderDetails)
                {
                    item.LocalPrice /= rate;

                    long productId = item.Product.Id;
                    DynamicParameters parameters = new DynamicParameters(new
                    {
                        orderId,
                        productId,
                        item.Quantity,
                        item.LocalPrice
                    });;

                    await connection.QueryAsync<long>(
                        SpName.OrderDetailsAdd,
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<Order> GetOrderWithDetailsByOrderId(long? orderId)
        {
            try
            {
                var orderDictionary = new Dictionary<long, Order>();
                var result = await connection.QueryAsync<Order, City, Order_Product, Product, Category, Order>(
                    SpName.OrderWithDetailsGetById, (o, c, op, p, cat) =>
                    {
                        Order order;
                        if (!orderDictionary.TryGetValue((long)o.Id, out order))
                        {
                            order = o;
                            order.OrderDetails = new List<Order_Product>();
                            orderDictionary.Add((long)order.Id, order);
                        }
                        order.City = c;
                        order.OrderDetails.Add(op);
                        op.Product = p;
                        p.Category = cat;
                        return order;
                    },
                    param: new { orderId },
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
