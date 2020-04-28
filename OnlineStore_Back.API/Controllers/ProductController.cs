using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Controllers;
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
        private readonly IOrderRepository _orderRepository;
        public ProductController(IProductRepository productRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetAllProducts()
        {
            var result = await _productRepository.GetAllProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Products not found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("{cityId}")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetCityById(long cityId)
        {
            {
                if (cityId < 1) return BadRequest("cityId can not be less than one.");
                var result = await _productRepository.GetCityById(cityId);
                if (result.IsOkay)
                {
                    if (result.RequestData == null) { return NotFound("City not found"); }
                    return Ok(_mapper.Map<CityOutputModel>(result.RequestData));
                }
                return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult<List<OrderOutputModel>>> AddOrder([FromBody] OrderInputModel inputModel)
        {
            var result = await _orderRepository.AddOrder(_mapper.Map<Order>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("City not found"); }
                return Ok(_mapper.Map<OrderOutputModel>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}