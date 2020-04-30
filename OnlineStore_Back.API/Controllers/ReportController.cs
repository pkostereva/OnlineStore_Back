using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineStoreBack.API.Models.OutputModels;
using OnlineStoreBack.Core;
using OnlineStoreBack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase, IReportController
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        // Ты хочешь узнать товары, которые никто ни разу не заказывал.
        [HttpGet("nobody-bought")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> Nobodybought()
        {
            ReportTypeEnum type = ReportTypeEnum.NobodyBought;
            var result = await _reportRepository.CallReport(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        // Ты хочешь узнать товары, по которым были продажи, но которых нет ни в представительствах, ни на Складе.
        [HttpGet("ordered-not-in-stock")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> OrderedNotInCity()
        {
            ReportTypeEnum type = ReportTypeEnum.OrderedNotInCity;
            var result = await _reportRepository.CallReport(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }


        //Ты хочешь узнать, какие товары есть на Складе, но при этом отсутствуют в СПб и Москве.
        [HttpGet("in-stock-not-in-cities")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> InStockNotInCIties()
        {
            ReportTypeEnum type = ReportTypeEnum.InStockNotInCities;
            var result = await _reportRepository.CallReport(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь увидеть информацию о продажах за определённый период времени: в таком-то городе было куплено столько то единиц такого-то товара за такую-то сумму.
        //[HttpGet("order/{startdate}/")]
        //public async ValueTask<ActionResult<List<ProductOutputModel>>> InStockNotInCIties()
        //{
        //    ReportTypeEnum type = ReportTypeEnum.InStockNotInCities;
        //    var result = await _reportRepository.CallReport(type);
        //    if (result.IsOkay)
        //    {
        //        if (result.RequestData == null) { return NotFound("Products not found"); }
        //        return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
        //    }
        //    return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        //}

        //курс валют
        [HttpGet("currency")]
        public async ValueTask<ActionResult<decimal>> Currency()
        {


            var result = await _reportRepository.SendingRequest();
            //var r = JsonSerializer.Deserialize<ExchangeRates>(result);
            //var rr = r.Valute.BYN.Values.ToArray();
            //return rr[];

            JObject o = JObject.Parse(result);
            JToken token = o.SelectToken("$.Valute.UAH.Value");
            decimal a = (decimal)token;
            return a;

            //return JsonSerializer.Deserialize<ExchangeRates>(result);
            //ReportTypeEnum type = ReportTypeEnum.InStockNotInCities;
            //var result = await _reportRepository.CallReport(type);
            //if (result.IsOkay)
            //{
            //    if (result.RequestData == null) { return NotFound("Products not found"); }
            //    return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            //}
            //return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }

    public class Reader
    {
        public string Date { get; set; }
        public string PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public string Timestamp { get; set; }
        public List<Dictionary<string, Valute>> Valute { get; set; }
    }
}