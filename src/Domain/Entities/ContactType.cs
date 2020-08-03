using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; }
    }
}
