using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("SalesOrderDetail", Schema = "Sales")]
    public class SalesOrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int SalesOrderID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public int SalesOrderDetailID { get; set; }
        
        [StringLength(25)]
        public string? CarrierTrackingNumber { get; set; }
        
        [Required]
        public short OrderQty { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        
        [Required]
        public int SpecialOfferID { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPriceDiscount { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "money")]
        public decimal? LineTotal { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual SalesOrderHeader SalesOrderHeader { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual SpecialOffer SpecialOffer { get; set; } = null!;
    }
}