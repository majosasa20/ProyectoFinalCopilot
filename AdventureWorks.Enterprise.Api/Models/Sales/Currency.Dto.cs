using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
    public class CurrencyDto
    {
        public string CurrencyCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
