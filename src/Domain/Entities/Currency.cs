using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        public  ICollection<CurrencyRate> CurrencyRateFromCurrencyCodeNavigation { get; set; }
        public  ICollection<CurrencyRate> CurrencyRateToCurrencyCodeNavigation { get; set; }
    }
}
