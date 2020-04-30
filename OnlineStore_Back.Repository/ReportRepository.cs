using Newtonsoft.Json.Linq;
using OnlineStoreBack.Core;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.DB.Storages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository
{
    public class ReportRepository : IReportRepository
    {
        private IReportStorage _reportStorage;
        public ReportRepository(IReportStorage reportStorage)
        {
            _reportStorage = reportStorage;
        }

        public async ValueTask<RequestResult<List<Product>>> CallReport(ReportTypeEnum type)
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                //_orderStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetProduct(type);
                //_leadStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                //_orderStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<string> SendingRequest()
        {
            WebRequest request = WebRequest.CreateHttp("https://www.cbr-xml-daily.ru/daily_json.js");
            Stream dataStream;
            WebResponse response = await request.GetResponseAsync();
            string result;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
            }
            response.Close();

            

            return result;
        }
    }
}
