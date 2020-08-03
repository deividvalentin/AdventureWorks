using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Domain.Entities;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Data.Tests
{
    public class SalesOrderHeaderRepositoryTests
    {
        private readonly Mock<IDbConnectionFactory> _mockConnectionFactory;
        private readonly Mock<IDbConnection> _mockDbConnection;

        private IList<SalesOrderHeader> _orders;

        public SalesOrderHeaderRepositoryTests()
        {
            _mockConnectionFactory = new Mock<IDbConnectionFactory>();
            _mockDbConnection = new Mock<IDbConnection>();


            _mockConnectionFactory.Setup(f => f.CreateDbConnection()).Returns(_mockDbConnection.Object);
        }

	    [Fact]
        public void GivenFirstPageSize2_WhenGetAllAsync_ReturnsCorrectly()
        {

	    }

	    [Fact]
        public void GivenSecondPageSize2_WhenGetAllAsync_ReturnsCorrectly()
        {

	    }

        [Fact]
        public void GivenFirstPageSize1_WhenGetAllAsync_MapsOrderDetailCorrectly()
        {

        }

        [Fact]
        public void GivenFirstPageSize1_WhenGetAllAsync_MapsSalesReasonCorrectly()
        {

        }

        [Fact]
        public void GivenInvalidPagination_WhenGetAllAsync_ReturnsEmpty()
        {

        }

        [Fact]
        public void GivenValidSalesOrderId_WhenGetByIdAsync_ReturnsCorrectly()
        {

        }

        [Fact]
        public void GivenValidSalesOrderId_WhenGetByIdAsync_MapsOrderDetailCorrectly()
        {

        }

        [Fact]
        public void GivenValidSalesOrderId_WhenGetByIdAsync_MapsSalesReasonCorrectly()
        {

        }

        [Fact]
        public void GivenInvalidSalesOrderId_WhenGetByIdAsync_ReturnsNull()
        {

        }

        [Fact]
        public void GivenValidOrders_WhenBulkInsert_DoesNotThrowsExeception()
        {

        }

        [Fact]
        public void GivenInvalidOrders_WhenBulkInsert_ThrowsExeception()
        {

        }

        [Fact]
        public void GivenValidOrders_WhenBulkUpdate_DoesNotThrowsExeception()
        {

        }

        [Fact]
        public void GivenInvalidOrders_WhenBulkUpdate_ThrowsExeception()
        {

        }

        [Fact]
        public void GivenValidOrders_WhenBulkDelete_DoesNotThrowsExeception()
        {

        }

        [Fact]
        public void GivenInvalidOrders_WhenBulkDelete_ThrowsExeception()
        {

        }

        private void LoadDummyData()
        {
            var ordersJson = File.ReadAllText(@".\Assets\orders.json");
            _orders = JsonConvert.DeserializeObject<List<SalesOrderHeader>>(ordersJson);
        }
    }
}