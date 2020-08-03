using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<ProductSubcategory> ProductSubcategories { get; set; }
    }
}
