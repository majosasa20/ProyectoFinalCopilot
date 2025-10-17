using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("CountryRegionCurrency", Schema = "Sales")]
    public class CountryRegionCurrency
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string CountryRegionCode { get; set; } = string.Empty;
        
        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CurrencyCode { get; set; } = string.Empty;
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Currency Currency { get; set; } = null!;
    }
}