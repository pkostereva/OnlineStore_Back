using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.DB.Storages
{
    public interface IReportStorage
    {
        ValueTask<List<Product>> GetProduct(ReportTypeEnum type);
    }
}