﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int StateProvinceId { get; set; }
        public string PostalCode { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddresses { get; set; }
        public ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddress { get; set; }
        public StateProvince StateProvince { get; set; }
    }
}
