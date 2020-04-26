using Microsoft.AspNetCore.Mvc;
using OnlineStore_Back.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_Back.API.Controllers
{
    public interface IProductController
    {
        ValueTask<ActionResult<List<ProductOutputModel>>> GetAllLeads();
    }
}