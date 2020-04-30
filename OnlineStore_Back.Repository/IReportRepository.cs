using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public interface IReportRepository
    {
        ValueTask<RequestResult<List<Product>>> CallReport(ReportTypeEnum type);
        ValueTask<string> SendingRequest();
    }
}