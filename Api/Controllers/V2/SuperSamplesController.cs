using System.Net.Mime;
using Api.Models.V2;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V2
{
    /// <summary>
    /// Controller Action approach.
    /// </summary>
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/super-samples")]
    public class SuperSamplesController : ControllerBase
    {
        [HttpPost("start")]
        public IActionResult StartProductionPlan()
        {
            return Ok();
        }

        [HttpGet]
        public SuperSample GetStatus()
        {
            return new SuperSample(Status.Inactive);
        }
    }
}
