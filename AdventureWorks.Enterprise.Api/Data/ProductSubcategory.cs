using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        
        [Required]
        public int ProductCategoryID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public Guid RowGuid { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}