using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("ProductInventory", Schema = "Production")]
    public class ProductInventory
    {
        [Key]
        [Column(Order = 0)]
        public int ProductID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public short LocationID { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Shelf { get; set; } = string.Empty;
        
        [Required]
        public byte Bin { get; set; }
        
        [Required]
        public short Quantity { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Product Product { get; set; } = null!;
    }
}