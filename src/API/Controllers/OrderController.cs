using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISalesOrderHeaderService _salesOrderHeaderService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(ISalesOrderHeaderService salesOrderHeaderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _salesOrderHeaderService = salesOrderHeaderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAsync([FromQuery] int pageIndex, int pageSize)
        {
            //GetAllAsync will return a tuple with totalRecords and a list of SalesOrderHeader
            var tupleOrders = await _salesOrderHeaderService.GetAllAsync(pageIndex, pageSize);

            if(tupleOrders == null)
                return NotFound();
            
            var totalRecords = tupleOrders.Item1;
            var ordersModel = _mapper.Map<IEnumerable<Order>>(tupleOrders.Item2);

            return Ok(new
            {
                data = new
                {
                    totalRecords = totalRecords,
                    list = ordersModel
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetAsync(int id)
        {
            //GetByIdAsync will return a list of SalesOrderHeader
            var order = await _salesOrderHeaderService.GetByIdAsync(id);
            
            if(order == null)
                return NotFound(id);
            
            // Convert to model 
            var orderModel = _mapper.Map<Order>(order);
            return Ok(new {
                data = orderModel
            });
        }

        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<Order> orders)
        {
            //Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseOrders = _mapper.Map<IEnumerable<SalesOrderHeader>>(orders);
            
            //Convert model to domain
            _salesOrderHeaderService.BulkInsert(responseOrders);

            return Created(nameof(GetAsync), new { pageIndex = 1, pageSize = 20});
 
        }

        [HttpPut]
        public ActionResult Put([FromBody] IEnumerable<Order> orders)
        {
            //Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Convert model to domain
            var responseOrders = _mapper.Map<IEnumerable<SalesOrderHeader>>(orders);

            _salesOrderHeaderService.BulkUpdate(responseOrders);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] IEnumerable<Order> orders)
        {
            //Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Convert model to domain
            var responseOrders = _mapper.Map<IEnumerable<SalesOrderHeader>>(orders);

            _salesOrderHeaderService.BulkDelete(responseOrders);
            return Ok();
        }
    }
}