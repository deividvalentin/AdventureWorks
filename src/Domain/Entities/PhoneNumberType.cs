using System;
using System.Collections.Generic;

namespace Domain.Entities
{    
    public class PhoneNumberType
    {
        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<PersonPhone> PersonPhones { get; set; }
    }
}
