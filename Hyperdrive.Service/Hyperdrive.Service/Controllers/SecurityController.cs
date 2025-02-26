using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Service.Controllers
{
    /// <summary>
    /// Represents a <see cref="SecurityController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>    
    /// <param name="service">Injected <see cref="ISecurityService"/></param>
    [Route("api/security")]
    [Produces("application/json")]
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    public class SecurityController(ISecurityService @service) : ControllerBase
    {
        /// <summary>
        /// Resets Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] SecurityPasswordReset @viewModel) => Ok(value: await @service.ResetPassword(@viewModel));

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordReset"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] SecurityPasswordChange @viewModel) => Ok(value: await @service.ChangePassword(@viewModel));

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityEmailChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changeemail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] SecurityEmailChange @viewModel) => Ok(value: await @service.ChangeEmail(@viewModel));

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPhoneNumberChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changephonenumber")]
        [Authorize]
        public async Task<IActionResult> ChangePhoneNumber([FromBody] SecurityPhoneNumberChange @viewModel) => Ok(value: await @service.ChangePhoneNumber(@viewModel));

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityNameChange"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("changename")]
        [Authorize]
        public async Task<IActionResult> ChangeName([FromBody] SecurityNameChange @viewModel) => Ok(value: await @service.ChangeName(@viewModel));
    }
}
