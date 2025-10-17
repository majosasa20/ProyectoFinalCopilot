using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("ProductModel", Schema = "Production")]
    public class ProductModel
    {
        [Key]
        public int ProductModelID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "xml")]
        public string? CatalogDescription { get; set; }
        
        [Column(TypeName = "xml")]
        public string? Instructions { get; set; }
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}