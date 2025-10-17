using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("SalesOrderHeader", Schema = "Sales")]
    public class SalesOrderHeader
    {
        [Key]
        public int SalesOrderID { get; set; }
        
        [Required]
        public byte RevisionNumber { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public DateTime? ShipDate { get; set; }
        
        [Required]
        public byte Status { get; set; }
        
        [Required]
        public bool OnlineOrderFlag { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(25)]
        public string? SalesOrderNumber { get; set; }
        
        [StringLength(25)]
        public string? PurchaseOrderNumber { get; set; }
        
        [StringLength(15)]
        public string? AccountNumber { get; set; }
        
        [Required]
        public int CustomerID { get; set; }
        
        public int? SalesPersonID { get; set; }
        
        public int? TerritoryID { get; set; }
        
        [Required]
        public int BillToAddressID { get; set; }
        
        [Required]
        public int ShipToAddressID { get; set; }
        
        [Required]
        public int ShipMethodID { get; set; }
        
        public int? CreditCardID { get; set; }
        
        [StringLength(15)]
        public string? CreditCardApprovalCode { get; set; }
        
        public int? CurrencyRateID { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal TaxAmt { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal Freight { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "money")]
        public decimal? TotalDue { get; set; }
        
        [StringLength(128)]
        public string? Comment { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual SalesPerson? SalesPerson { get; set; }
        public virtual SalesTerritory? SalesTerritory { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    }
}