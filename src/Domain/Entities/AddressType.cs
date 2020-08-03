using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AddressType
    {
        public int AddressTypeId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; }
    }
}
