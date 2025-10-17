using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Enterprise.Api.Data
{
    [Table("JobCandidate", Schema = "HumanResources")]
    public class JobCandidate
    {
        [Key]
        public int JobCandidateID { get; set; }
        
        public int? BusinessEntityID { get; set; }
        
        [Column(TypeName = "xml")]
        public string? Resume { get; set; }
        
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}