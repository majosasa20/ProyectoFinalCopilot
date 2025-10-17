using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("WorkOrder", Schema = "Production")]
    public class WorkOrder
    {
        [Key]
        public int WorkOrderID { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        
        [Required]
        public int OrderQty { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StockedQty { get; set; }
        
        [Required]
        public short ScrappedQty { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public short? ScrapReasonID { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual Product Product { get; set; } = null!;
    }
}