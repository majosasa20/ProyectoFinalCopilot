using Microsoft.AspNetCore.Mvc;
using AdventureWorks.Enterprise.WebApp.Data.HumanResources;
using AdventureWorks.Enterprise.WebApp.Models.HumanResources;

namespace AdventureWorks.Enterprise.WebApp.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(DepartmentData.Departments);
    }
}
