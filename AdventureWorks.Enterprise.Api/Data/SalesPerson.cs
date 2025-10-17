using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("SalesPerson", Schema = "Sales")]
    public class SalesPerson
    {
        [Key]
        public int BusinessEntityID { get; set; }
        
        public int? TerritoryID { get; set; }
        
        [Column(TypeName = "money")]
        public decimal? SalesQuota { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal Bonus { get; set; }
        
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal CommissionPct { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal SalesYTD { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal SalesLastYear { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual SalesTerritory? SalesTerritory { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
    }
}