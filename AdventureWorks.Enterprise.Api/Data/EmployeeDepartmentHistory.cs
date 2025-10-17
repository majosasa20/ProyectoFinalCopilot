using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("EmployeeDepartmentHistory", Schema = "HumanResources")]
    public class EmployeeDepartmentHistory
    {
        [Key]
        [Column(Order = 0)]
        public int BusinessEntityID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public DateTime StartDate { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public short DepartmentID { get; set; }
        
        [Key]
        [Column(Order = 3)]
        public byte ShiftID { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Department Department { get; set; } = null!;
        public virtual Shift Shift { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}