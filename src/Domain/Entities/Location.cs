﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Location
    {
        public short LocationId { get; set; }
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<ProductInventory> ProductInventory { get; set; }
        public ICollection<WorkOrderRouting> WorkOrderRoutings { get; set; }
    }
}
