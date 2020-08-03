using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductDescription
    {
        public int ProductDescriptionId { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
    }
}
