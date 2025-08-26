using Hyperdrive.Tier.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Hyperdrive.Application.ViewModels.Security;
using MediatR;

namespace Hyperdrive.Service.Controllers
{
    /// <summary>
    /// Represents a <see cref="SecurityController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>    
    /// <param name="mediator">Injected <see cref="IMediator"/></param>
    [Route("api/security")]
    [Produces("application/json")]
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    public class SecurityController(IMediator @mediator) : ControllerBase
    {
        /// <summary>
        /// Resets Password
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityPasswordChange"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] SecurityPasswordReset @viewModel) => Ok(value: await @service.ResetPassword(@viewModel));

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityPasswordReset"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] SecurityPasswordChange @viewModel) => Ok(value: await @service.ChangePassword(@viewModel));

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityEmailChange"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("changeemail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] SecurityEmailChange @viewModel) => Ok(value: await @service.ChangeEmail(@viewModel));

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityPhoneNumberChange"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("changephonenumber")]
        [Authorize]
        public async Task<IActionResult> ChangePhoneNumber([FromBody] SecurityPhoneNumberChange @viewModel) => Ok(value: await @service.ChangePhoneNumber(@viewModel));

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityNameChange"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("changename")]
        [Authorize]
        public async Task<IActionResult> ChangeName([FromBody] SecurityNameChange @viewModel) => Ok(value: await @service.ChangeName(@viewModel));

        /// <summary>
        /// Refreshes Tokens
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
        [HttpPut]
        [Route("refreshtokens")]
        public async Task<IActionResult> RefreshTokens([FromBody] SecurityRefreshTokenReset @viewModel) => Ok(value: await @service.RefreshTokens(@viewModel));
    }
}
