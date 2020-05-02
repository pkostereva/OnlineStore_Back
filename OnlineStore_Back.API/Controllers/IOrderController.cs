using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    public interface IOrderController
    {
        ValueTask<ActionResult<List<OrderOutputModel>>> AddOrder([FromBody] OrderInputModel inputModel);
    }
}