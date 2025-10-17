using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public int? PersonID { get; set; }
        public int? StoreID { get; set; }
        public int? TerritoryID { get; set; }
        public string? AccountNumber { get; set; } // Computed
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
