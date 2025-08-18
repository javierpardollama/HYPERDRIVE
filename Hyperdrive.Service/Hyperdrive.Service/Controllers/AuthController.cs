using Hyperdrive.Tier.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Hyperdrive.Application.ViewModels.Auth;

namespace Hyperdrive.Service.Controllers
{
    /// <summary>
    /// Represents a <see cref="AuthController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>   
    /// <param name="service">Injected <see cref="IAuthService"/></param>
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    public class AuthController(IAuthService @service) : ControllerBase
    {
        /// <summary>
        /// Signs In
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="AuthSignIn"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthSignIn @viewModel) => Ok(value: await @service.SignIn(@viewModel));

        /// <summary>
        /// Joins In
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPost]
        [Route("joinin")]
        public async Task<IActionResult> JoinIn([FromBody] AuthJoinIn @viewModel) => Ok(value: await @service.JoinIn(@viewModel));

        /// <summary>
        /// Signs Out
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="AuthSignOut"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPost]
        [Route("signout")]
        public async Task<IActionResult> SignOut([FromBody] AuthSignOut @viewModel)
        {
            await @service.SignOut(@viewModel);

            return Ok();
        }
    }
}
