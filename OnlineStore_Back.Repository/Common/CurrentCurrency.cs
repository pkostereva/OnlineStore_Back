using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreBack.Repository.Common
{
    public static class CurrentCurrency
    {
        public static async ValueTask<decimal> GetCurrency(string path)
        {
            WebRequest request = WebRequest.CreateHttp("https://www.cbr-xml-daily.ru/daily_json.js");
            Stream dataStream;
            WebResponse response = await request.GetResponseAsync();
            string excRate;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                excRate = reader.ReadToEnd();
            }
            response.Close();
            JObject currency = JObject.Parse(excRate);
            var exchangeRate = (decimal)currency.SelectToken($"$.Valute.{path}.Value");
            return exchangeRate;
        }
    }
}
