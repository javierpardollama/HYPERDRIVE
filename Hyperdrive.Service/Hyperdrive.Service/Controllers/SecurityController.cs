using System.Threading.Tasks;

using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Security;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Tier.Web.Controllers
{
    /// <summary>
    /// Represents a <see cref="SecurityController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>    
    /// <param name="service">Injected <see cref="ISecurityService"/></param>
    [Route("api/security")]
    [Produces("application/json")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class SecurityController(ISecurityService @service) : ControllerBase
    {
        /// <summary>
        /// Resets Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]SecurityPasswordReset @viewModel) => new JsonResult(value: await @service.ResetPassword(@viewModel));

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="viewModel">njected <see cref="SecurityPasswordReset"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]SecurityPasswordChange @viewModel) => new JsonResult(value: await @service.ChangePassword(@viewModel));

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="viewModel">njected <see cref="SecurityEmailChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changeemail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody]SecurityEmailChange @viewModel) => new JsonResult(value: await @service.ChangeEmail(@viewModel));
    }
}
