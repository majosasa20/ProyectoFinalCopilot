using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("SpecialOffer", Schema = "Sales")]
    public class SpecialOffer
    {
        [Key]
        public int SpecialOfferID { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal DiscountPct { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public int MinQty { get; set; }
        
        public int? MaxQty { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    }
}