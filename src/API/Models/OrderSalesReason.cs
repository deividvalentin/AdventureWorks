using System;

namespace API.Models
{
    public class OrderSalesReason
    {
        public int SalesOrderId { get; set; }
        public int SalesReasonId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public SalesReason SalesReason { get; set; }
    }
}