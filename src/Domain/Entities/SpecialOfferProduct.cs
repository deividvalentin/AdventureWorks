using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SpecialOfferProduct
    {
        public int SpecialOfferId { get; set; }
        public int ProductId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
        public Product Product { get; set; }
        public SpecialOffer SpecialOffer { get; set; }
    }
}
