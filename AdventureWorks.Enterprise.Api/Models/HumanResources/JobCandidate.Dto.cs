using System;

namespace AdventureWorks.Enterprise.Api.Models.HumanResources
{
    public class JobCandidateDto
    {
        public int JobCandidateID { get; set; }
        public int? BusinessEntityID { get; set; }
        public string? Resume { get; set; } // XML as string
        public DateTime ModifiedDate { get; set; }
    }
}
