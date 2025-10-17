using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("Customer", Schema = "Sales")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        
        public int? PersonID { get; set; }
        
        public int? StoreID { get; set; }
        
        public int? TerritoryID { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(10)]
        public string? AccountNumber { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Store? Store { get; set; }
        public virtual SalesTerritory? SalesTerritory { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
    }
}