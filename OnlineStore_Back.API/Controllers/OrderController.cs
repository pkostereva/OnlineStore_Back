using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.Repository;

namespace OnlineStoreBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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