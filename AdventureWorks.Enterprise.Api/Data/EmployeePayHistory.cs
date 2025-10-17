using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("EmployeePayHistory", Schema = "HumanResources")]
    public class EmployeePayHistory
    {
        [Key]
        [Column(Order = 0)]
        public int BusinessEntityID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public DateTime RateChangeDate { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal Rate { get; set; }
        
        [Required]
        public byte PayFrequency { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}