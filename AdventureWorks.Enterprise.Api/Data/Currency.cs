using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("Currency", Schema = "Sales")]
    public class Currency
    {
        [Key]
        [StringLength(3)]
        public string CurrencyCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<CountryRegionCurrency> CountryRegionCurrencies { get; set; } = new List<CountryRegionCurrency>();
    }
}