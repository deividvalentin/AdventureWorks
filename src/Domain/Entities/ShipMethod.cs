using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ShipMethod
    {
        public int ShipMethodId { get; set; }
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
