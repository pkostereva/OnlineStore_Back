using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    public interface IReportController
    {
        ValueTask<ActionResult<List<ProductOutputModel>>> InStockNotInCIties();
        ValueTask<ActionResult<List<ProductOutputModel>>> Nobodybought();
        ValueTask<ActionResult<List<ProductOutputModel>>> OrderedNotInCity();
    }
}