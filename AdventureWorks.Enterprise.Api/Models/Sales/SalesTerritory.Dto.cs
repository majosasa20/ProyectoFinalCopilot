using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
    public class SalesTerritoryDto
    {
        public int TerritoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryRegionCode { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYTD { get; set; }
        public decimal CostLastYear { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
