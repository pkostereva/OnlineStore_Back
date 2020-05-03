using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("search")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> ProductSearch(ProductSearchInputModel inputModel)
        {

            var result = await _productRepository.ProductSearch(_mapper.Map<ProductSearch>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}