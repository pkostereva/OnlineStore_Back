using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    public interface IReportController
    {
        ValueTask<ActionResult<List<CityTotalWorthOutputModel>>> GetTotalWorthByCity();
        ValueTask<ActionResult<List<OrderWideOutputModel>>> GetOrdersByTimeSpan(DatesInputModel model);
        ValueTask<ActionResult<List<MostSoldProductInCityOutputModel>>> GetMostSoldProductInCities();
        ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsInStockNotInCities();
        ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsOrderedButNotInCities();
        ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsNeverOrdered();
        ValueTask<ActionResult<List<CategoriesWithCountOfProductsOutputModel>>> GetCategoriesWithFiveAndMoreProducts();
        ValueTask<ActionResult<List<TotalCostOutputModel>>> GetTotalCostByCountry();
    }
}