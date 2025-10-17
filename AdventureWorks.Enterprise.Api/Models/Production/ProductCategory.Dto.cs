using System;

namespace AdventureWorks.Enterprise.Api.Models.Production
{
    public class ProductCategoryDto
    {
        public int ProductCategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
