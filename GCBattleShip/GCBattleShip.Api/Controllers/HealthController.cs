using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GCBattleShip.Api.Controllers
{
    [Route("health")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [HttpHead]
        [Route("ping")]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}