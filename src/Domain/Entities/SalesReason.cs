using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SalesReason
    {
        public int SalesReasonId { get; set; }
        public string Name { get; set; }
        public string ReasonType { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
    }
}
