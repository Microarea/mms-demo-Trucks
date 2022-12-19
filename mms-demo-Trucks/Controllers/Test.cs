using Microsoft.AspNetCore.Mvc;

namespace MMSDemoTrucks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Test")]
    public class Test : ControllerBase
    {
        [HttpGet]
        [Route("api/assemblyversion")]
        //-----------------------------------------------------------------------------	
        public IActionResult ApiAssemblyVersion()
        {
            return new OkObjectResult("1.0.0");
        }
    }
}
