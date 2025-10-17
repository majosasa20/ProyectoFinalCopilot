using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
    public class CountryRegionCurrencyDto
    {
        public string CountryRegionCode { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
