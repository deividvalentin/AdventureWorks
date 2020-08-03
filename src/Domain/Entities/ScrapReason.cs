using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ScrapReason
    {

        public short ScrapReasonId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
