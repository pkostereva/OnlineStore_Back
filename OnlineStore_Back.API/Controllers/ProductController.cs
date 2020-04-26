using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_Back.API.Models.OutputModels;
using OnlineStore_Back.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_Back.API.Controllers
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

        [HttpGet]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetAllLeads()
        {
            var result = await _productRepository.GetAllProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}