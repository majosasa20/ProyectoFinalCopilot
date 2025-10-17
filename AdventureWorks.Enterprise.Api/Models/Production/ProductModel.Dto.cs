using System;

namespace AdventureWorks.Enterprise.Api.Models.Production
{
    public class ProductModelDto
    {
        public int ProductModelID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CatalogDescription { get; set; } // XML as string
        public string? Instructions { get; set; } // XML as string
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
