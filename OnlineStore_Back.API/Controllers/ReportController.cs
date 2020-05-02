using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using OnlineStoreBack.Core;
using OnlineStoreBack.Repository;
using System;
using System.Collections.Generic;
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

        //Сколько у тебя гипотетических денег в каждом городе, а также на складе.
        [HttpGet("city-total-worth")]
        public async ValueTask<ActionResult<List<CityTotalWorthOutputModel>>> GetTotalWorthByCity()
        {
            var result = await _reportRepository.GetTotalWorthByCity();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return BadRequest("Something goes wrong"); }
                return Ok(_mapper.Map<List<CityTotalWorthOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь увидеть информацию о продажах за определённый период времени: в таком-то городе
        //было куплено столько то единиц такого-то товара за такую-то сумму.
        [HttpGet("orders-between-dates")]
        public async ValueTask<ActionResult<List<OrderWideOutputModel>>> GetOrdersByTimeSpan(DatesInputModel model)
        {
            DateTime start = Convert.ToDateTime(model.Start);
            DateTime end = Convert.ToDateTime(model.End);
            var result = await _reportRepository.GetOrdersByTimeSpan(start, end);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Orders not found"); }
                return Ok(_mapper.Map<List<OrderWideOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь узнать самый часто продаваемый товар в каждом городе.
        [HttpGet("most-sold")]
        public async ValueTask<ActionResult<List<MostSoldProductInCityOutputModel>>> GetMostSoldProductInCities()
        {
            var result = await _reportRepository.GetMostSoldProductInCities();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return BadRequest("Something goes wrong"); }
                return Ok(_mapper.Map<List<MostSoldProductInCityOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь узнать, какие товары есть на Складе, но при этом отсутствуют в СПб и Москве.
        [HttpGet("in-stock-not-in-cities")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsInStockNotInCities()
        {
            ReportTypeEnum type = ReportTypeEnum.ProductsInStockButNotInCities;
            var result = await _reportRepository.GetFilteredProducts(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        // Ты хочешь узнать товары, по которым были продажи, но которых нет ни в представительствах, ни на Складе.
        [HttpGet("ordered-not-in-stock")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsOrderedButNotInCities()
        {
            ReportTypeEnum type = ReportTypeEnum.ProductsOrderedButNotInCities;
            var result = await _reportRepository.GetFilteredProducts(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        // Ты хочешь узнать товары, которые никто ни разу не заказывал.
        [HttpGet("nobody-bought")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsNeverOrdered()
        {
            ReportTypeEnum type = ReportTypeEnum.ProductsNeverOrdered;
            var result = await _reportRepository.GetFilteredProducts(type);
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь узнать категории, в которых заведено 5 и более разных товаров.
        [HttpGet("categories-five-products")]
        public async ValueTask<ActionResult<List<CategoriesWithCountOfProductsOutputModel>>> GetCategoriesWithFiveAndMoreProducts()
        {
            var result = await _reportRepository.GetCategoriesWithFiveAndMoreProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Categories not found"); }
                return Ok(_mapper.Map<List<CategoriesWithCountOfProductsOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //Ты хочешь узнать сумму заказов внутри РФ и за рубежом 
        //(должна вывестись одна строка с двумя столбцами: Продажи в РФ, Продажи за рубежом).
        [HttpGet("sales-russia-and-world")]
        public async ValueTask<ActionResult<List<TotalCostOutputModel>>> GetTotalCostByCountry()
        {
            var result = await _reportRepository.GetTotalCostByCountry();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return BadRequest("Something goes wrong"); }
                return Ok(_mapper.Map<TotalCostOutputModel>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}