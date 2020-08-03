using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Z.Dapper.Plus;

namespace Data.Repositories
{
    public class SalesOrderHeaderRepository : ISalesOrderHeaderRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;

        //A static constructor is used to initialize the Mapping to Database
        static SalesOrderHeaderRepository()
        {
            DapperPlusManager.Entity<SalesOrderHeader>().Table($"Sales.{nameof(SalesOrderHeader)}")
                                                        .Identity(x => x.SalesOrderId)
                                                        .Ignore(x => new { x.SalesOrderNumber, x.TotalDue });

            DapperPlusManager.Entity<SalesOrderDetail>().Table($"Sales.{nameof(SalesOrderDetail)}")
                                                        .Identity(x => x.SalesOrderDetailId)
                                                        .Ignore(x => new { x.LineTotal });
        }

        public SalesOrderHeaderRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory; 
        }
        
        public async Task<Tuple<int,IEnumerable<SalesOrderHeader>>> GetAllAsync(int pageIndex, int pageSize)
        {
            using var dbContext = _dbConnectionFactory.CreateDbConnection();

            if (dbContext.State == ConnectionState.Closed)
                dbContext.Open();

            var queryTotalRecords = @"SELECT COUNT(*) FROM Sales.SalesOrderHeader Orders WITH(NOLOCK)";
           
            var querySalesOrderHeader = @"SELECT * FROM Sales.SalesOrderHeader Orders WITH(NOLOCK)
                            ORDER BY Orders.OrderDate DESC
                            OFFSET @pageSize * (@pageIndex - 1) ROWS
                            FETCH NEXT @pageSize ROWS ONLY";

            var querySalesOrderDetails = @"SELECT OrderDetails.SalesOrderDetailID,
                                OrderDetails.SalesOrderID,
                                OrderDetails.CarrierTrackingNumber,
                                OrderDetails.OrderQty,
                                OrderDetails.ProductID,
                                OrderDetails.SpecialOfferID,
                                OrderDetails.UnitPrice,
                                OrderDetails.UnitPriceDiscount,
                                OrderDetails.LineTotal,
                                OrderDetails.rowguid,
                                OrderDetails.ModifiedDate,
                                Product.ProductID,
                                Product.Name,
                                Product.ProductNumber,
                                Product.MakeFlag,
                                Product.FinishedGoodsFlag,
                                Product.Color,
                                Product.SafetyStockLevel,
                                Product.ReorderPoint,
                                Product.StandardCost,
                                Product.ListPrice,
                                Product.Size,
                                Product.SizeUnitMeasureCode,
                                Product.WeightUnitMeasureCode,
                                Product.Weight,
                                Product.DaysToManufacture,
                                Product.ProductLine,
                                Product.Class,
                                Product.Style,
                                Product.ProductSubcategoryID,
                                Product.ProductModelID,
                                Product.SellStartDate,
                                Product.SellEndDate,
                                Product.DiscontinuedDate,
                                Product.rowguid,
                                Product.ModifiedDate
                            FROM Sales.SalesOrderDetail OrderDetails WITH(NOLOCK)
                            INNER JOIN Production.Product Product WITH(NOLOCK) ON OrderDetails.ProductID = Product.ProductID
                            WHERE OrderDetails.SalesOrderID IN @salesOrderIds";
            
            var querySalesOrderHeaderSalesReason = @"SELECT SSR.SalesOrderID
                            ,SSR.SalesReasonID
                            ,SSR.ModifiedDate
                            ,SR.SalesReasonID
                            ,SR.Name
                            ,SR.ReasonType
                            ,SR.ModifiedDate
                        FROM [Sales].[SalesOrderHeaderSalesReason] SSR WITH(NOLOCK)
                        INNER JOIN [Sales].[SalesReason] SR WITH(NOLOCK) ON SSR.SalesReasonID = SR.SalesReasonID WHERE SSR.SalesOrderID IN @salesOrderIds";
            
            // get the total records of SalesOrderHeader
            var totalRecords = await dbContext.QueryFirstAsync<int>(queryTotalRecords);
            
            // get order by the provided salesOrderId
            var salesOrderHeaders = await dbContext.QueryAsync<SalesOrderHeader>(querySalesOrderHeader,  param: new { pageIndex, pageSize });
            var salesOrderIds = salesOrderHeaders.Select(o => o.SalesOrderId);
            
            // get order detailsfor orders returned from pagination criteria
            var salesOrderDetails = await dbContext.QueryAsync<SalesOrderDetail, Product, SalesOrderDetail>(querySalesOrderDetails,
            (orderDetail, product) =>
            {
                SalesOrderDetail orderDetailEntry;
                orderDetailEntry = orderDetail;
                orderDetailEntry.Product = product;
                return orderDetailEntry;
            }, new { salesOrderIds }, splitOn: "SalesOrderDetailID,ProductId");

            // get sales reason for orders returned from pagination criteria
            var salesOrderHeaderSalesReasons = await dbContext.QueryAsync<SalesOrderHeaderSalesReason, SalesReason, SalesOrderHeaderSalesReason>(querySalesOrderHeaderSalesReason,
            (salesOrderHeaderSalesReason,salesReason) =>
            {
                salesOrderHeaderSalesReason.SalesReason = salesReason;
                return salesOrderHeaderSalesReason;
            }, param:  new { salesOrderIds }, splitOn: "SalesOrderID,SalesReasonID");

            // map details and reason to orders returned from pagination criteria
            foreach (var salesOrder in salesOrderHeaders)
            {
                salesOrder.SalesOrderDetails = salesOrderDetails.Where(od => od.SalesOrderId == salesOrder.SalesOrderId).ToList();
                salesOrder.SalesOrderHeaderSalesReasons = salesOrderHeaderSalesReasons.Where(os => os.SalesOrderId == salesOrder.SalesOrderId).ToList();
            }
            
            return Tuple.Create(totalRecords, salesOrderHeaders);
        }

        public async Task<SalesOrderHeader> GetByIdAsync(int id)
        {
            using var dbContext = _dbConnectionFactory.CreateDbConnection();

            if (dbContext.State == ConnectionState.Closed)
                dbContext.Open();

            var querySalesOrderHeader = @"SELECT * FROM Sales.SalesOrderHeader Orders WITH(NOLOCK) WHERE Orders.SalesOrderID = @id";

            var querySalesOrderDetails = @"SELECT OrderDetails.SalesOrderDetailID,
                                OrderDetails.SalesOrderID,
                                OrderDetails.CarrierTrackingNumber,
                                OrderDetails.OrderQty,
                                OrderDetails.ProductID,
                                OrderDetails.SpecialOfferID,
                                OrderDetails.UnitPrice,
                                OrderDetails.UnitPriceDiscount,
                                OrderDetails.LineTotal,
                                OrderDetails.rowguid,
                                OrderDetails.ModifiedDate,
                                Product.ProductID,
                                Product.Name,
                                Product.ProductNumber,
                                Product.MakeFlag,
                                Product.FinishedGoodsFlag,
                                Product.Color,
                                Product.SafetyStockLevel,
                                Product.ReorderPoint,
                                Product.StandardCost,
                                Product.ListPrice,
                                Product.Size,
                                Product.SizeUnitMeasureCode,
                                Product.WeightUnitMeasureCode,
                                Product.Weight,
                                Product.DaysToManufacture,
                                Product.ProductLine,
                                Product.Class,
                                Product.Style,
                                Product.ProductSubcategoryID,
                                Product.ProductModelID,
                                Product.SellStartDate,
                                Product.SellEndDate,
                                Product.DiscontinuedDate,
                                Product.rowguid,
                                Product.ModifiedDate
                            FROM Sales.SalesOrderDetail OrderDetails WITH(NOLOCK)
                            INNER JOIN Production.Product Product WITH(NOLOCK) ON OrderDetails.ProductID = Product.ProductID
                            WHERE OrderDetails.SalesOrderID IN @SalesOrderId";
            
            var querySalesOrderHeaderSalesReason = @"SELECT SSR.SalesOrderID
                            ,SSR.SalesReasonID
                            ,SSR.ModifiedDate
                            ,SR.SalesReasonID
                            ,SR.Name
                            ,SR.ReasonType
                            ,SR.ModifiedDate
                        FROM [Sales].[SalesOrderHeaderSalesReason] SSR WITH(NOLOCK)
                        INNER JOIN [Sales].[SalesReason] SR WITH(NOLOCK) ON SSR.SalesReasonID = SR.SalesReasonID WHERE SSR.SalesOrderID IN @orderDetailSalesOrderIds";
            
            // get order by the provided salesOrderId
            var salesOrderHeader = await dbContext.QueryFirstAsync<SalesOrderHeader>(querySalesOrderHeader,  param: new { id });
            
            // get sales reason for orders returned from pagination criteria
            var salesOrderDetails = await dbContext.QueryAsync<SalesOrderDetail, Product, SalesOrderDetail>(querySalesOrderDetails,
            (orderDetail, product) =>
            {
                SalesOrderDetail orderDetailEntry;
                orderDetailEntry = orderDetail;
                orderDetailEntry.Product = product;
                return orderDetailEntry;
            }, new { salesOrderHeader.SalesOrderId }, splitOn: "SalesOrderDetailID,ProductId");
            
            // map details and reason to orders returned from pagination criteria
            var salesOrderHeaderSalesReasons = await dbContext.QueryAsync<SalesOrderHeaderSalesReason, SalesReason, SalesOrderHeaderSalesReason>(querySalesOrderHeaderSalesReason,
            (salesOrderHeaderSalesReason,salesReason) =>
            {
                salesOrderHeaderSalesReason.SalesReason = salesReason;
                return salesOrderHeaderSalesReason;
            }, param:  new { salesOrderHeader.SalesOrderId }, splitOn: "SalesOrderID,SalesReasonID");

            salesOrderHeader.SalesOrderDetails = salesOrderDetails.Where(od => od.SalesOrderId == salesOrderHeader.SalesOrderId).ToList();
            salesOrderHeader.SalesOrderHeaderSalesReasons = salesOrderHeaderSalesReasons.Where(os => os.SalesOrderId == salesOrderHeader.SalesOrderId).ToList();
            
            return salesOrderHeader;
        }

        public void BulkInsert(IEnumerable<SalesOrderHeader> entityList)
        {
            using var dbContext = _dbConnectionFactory.CreateDbConnection();

            if (dbContext.State == ConnectionState.Closed)
                dbContext.Open();

            try
            {
                //Insert multiple records and dependencies in one transaction
                dbContext.BulkInsert(entityList).ThenForEach(x => x.SalesOrderDetails.ToList().ForEach(y => y.SalesOrderId = x.SalesOrderId)).ThenBulkInsert(x => x.SalesOrderDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BulkUpdate(IEnumerable<SalesOrderHeader> entityList)
        {
            using var dbContext = _dbConnectionFactory.CreateDbConnection();

            if (dbContext.State == ConnectionState.Closed)
                dbContext.Open();

            try
            {
                //Update multiple records and dependencies in one transaction
                dbContext.BulkUpdate(entityList, o => o.SalesOrderDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BulkDelete(IEnumerable<SalesOrderHeader> entityList)
        {
            using var dbContext = _dbConnectionFactory.CreateDbConnection();

            if (dbContext.State == ConnectionState.Closed)
                dbContext.Open();

            try
            {
                //Delete multiple records and dependencies in one transaction
                dbContext.BulkDelete(entityList.SelectMany(o => o.SalesOrderDetails)).BulkDelete(entityList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}