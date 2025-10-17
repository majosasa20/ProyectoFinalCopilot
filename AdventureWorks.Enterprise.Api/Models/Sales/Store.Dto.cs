using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
    public class StoreDto
    {
        public int BusinessEntityID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? SalesPersonID { get; set; }
        public string? Demographics { get; set; } // XML as string
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
