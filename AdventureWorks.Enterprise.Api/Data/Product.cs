using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; } = string.Empty;
        
        [Required]
        public bool MakeFlag { get; set; }
        
        [Required]
        public bool FinishedGoodsFlag { get; set; }
        
        [StringLength(15)]
        public string? Color { get; set; }
        
        [Required]
        public short SafetyStockLevel { get; set; }
        
        [Required]
        public short ReorderPoint { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal StandardCost { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal ListPrice { get; set; }
        
        [StringLength(5)]
        public string? Size { get; set; }
        
        [StringLength(3)]
        public string? SizeUnitMeasureCode { get; set; }
        
        [StringLength(3)]
        public string? WeightUnitMeasureCode { get; set; }
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal? Weight { get; set; }
        
        [Required]
        public int DaysToManufacture { get; set; }
        
        [StringLength(2)]
        public string? ProductLine { get; set; }
        
        [StringLength(2)]
        public string? Class { get; set; }
        
        [StringLength(2)]
        public string? Style { get; set; }
        
        public int? ProductSubcategoryID { get; set; }
        
        public int? ProductModelID { get; set; }
        
        [Required]
        public DateTime SellStartDate { get; set; }
        
        public DateTime? SellEndDate { get; set; }
        
        public DateTime? DiscontinuedDate { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ProductSubcategory? ProductSubcategory { get; set; }
        public virtual ProductModel? ProductModel { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    }
}