using System;

namespace Domain.Entities
{
    public class EmailAddress
    {
        public int BusinessEntityId { get; set; }
        public int EmailAddressId { get; set; }
        public string EmailAddressName { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  Person BusinessEntity { get; set; }
    }
}
