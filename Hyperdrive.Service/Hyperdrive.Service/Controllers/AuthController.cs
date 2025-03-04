using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

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
        /// <param name="viewModel">Injected <see cref="AuthSignIn"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthSignIn @viewModel) => Ok(value: await @service.SignIn(@viewModel));

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPost]
        [Route("joinin")]
        public async Task<IActionResult> JoinIn([FromBody] AuthJoinIn @viewModel) => Ok(value: await @service.JoinIn(@viewModel));

        /// <summary>
        /// Signs Out
        /// </summary>
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
