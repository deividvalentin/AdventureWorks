using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Culture
    {
        public string CultureId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
    }
}
