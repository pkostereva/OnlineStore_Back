using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    public interface IProductController
    {
        ValueTask<ActionResult<List<ProductOutputModel>>> ProductSearch(ProductSearchInputModel inputModel);
    }
}