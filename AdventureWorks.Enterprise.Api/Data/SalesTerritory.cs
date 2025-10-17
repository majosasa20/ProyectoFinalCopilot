using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("SalesTerritory", Schema = "Sales")]
    public class SalesTerritory
    {
        [Key]
        public int TerritoryID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(3)]
        public string CountryRegionCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Group { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "money")]
        public decimal SalesYTD { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal SalesLastYear { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal CostYTD { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal CostLastYear { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<SalesPerson> SalesPersons { get; set; } = new List<SalesPerson>();
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
    }
}