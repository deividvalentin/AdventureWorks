using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class CountryRegion
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        public  ICollection<SalesTerritory> SalesTerritory { get; set; }
        public  ICollection<StateProvince> StateProvince { get; set; }
    }
}
