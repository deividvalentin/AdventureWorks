using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests
{
    [Trait("Category", "API")]
    public class OrderControllerTests
    {
        private readonly Mock<ISalesOrderHeaderService> _mockSalesOrderHeaderService;
        private readonly Mock<ILogger<OrderController>> _logger;
        private IList<SalesOrderHeader> _orders;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mockSalesOrderHeaderService = new Mock<ISalesOrderHeaderService>();
            _logger = new Mock<ILogger<OrderController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SalesOrderDetail, OrderDetail>().ReverseMap();
                cfg.CreateMap<SalesOrderHeader, Order>()
                    .ForMember(o => o.OrderDetails, o => o.MapFrom(src => src.SalesOrderDetails))
                    .ReverseMap();
            });

            _orderController = new OrderController(_mockSalesOrderHeaderService.Object, config.CreateMapper(), _logger.Object);
            LoadDummyData();

        }

        [Fact]
        public void When_GetAllOrders_By_Pagination()
        {
            //Arrange
            _mockSalesOrderHeaderService.Setup(d => d.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                                        .Returns(Task.FromResult(Tuple.Create(10,_orders.Take(10))));
                
            //Act
            var result = _orderController.GetAsync(1, 10).Result;
            var data = result.Value;

            //Assert
            Assert.NotNull(data);
            //Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Given_Order_ThenReturns_SalesOrderDetailQuantity()
        {
            //Arrange
            _mockSalesOrderHeaderService.Setup(d => d.GetByIdAsync(It.IsAny<int>()))
                                        .Returns(Task.FromResult(_orders.Last()));
                
            //Act
            var response = _orderController.GetAsync(It.IsAny<int>()).Result;
            var result = response.Result as OkObjectResult;
            var data =  result.Value;


            //Assert
            Assert.NotNull(result);
            //Assert.True(result.Value..Count() > 0);
        }

        [Fact]
        public void Given_Order_ThenReturns_SalesOrderDetailLineTotal()
        { 
        }

         [Fact]
        public void Given_Order_WhenAddMultipleOrders()
        { 
            //Arrange
            _mockSalesOrderHeaderService.Setup(d => d.BulkInsert(It.IsAny<IEnumerable<SalesOrderHeader>>()));
        }


        private void LoadDummyData()
        {
            var ordersJson = File.ReadAllText(@".\Assets\orders.json");
            _orders = JsonConvert.DeserializeObject<List<SalesOrderHeader>>(ordersJson);
        }
    }
}
