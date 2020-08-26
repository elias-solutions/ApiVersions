using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3
{
    [ApiController]
    [Route("api/v{version:apiVersion}/ultimate-samples")]
    [ApiVersion("3.0")]
    public class UltimateSamplesController : ControllerBase
    {
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}