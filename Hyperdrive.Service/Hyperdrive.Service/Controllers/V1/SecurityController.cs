using System.Threading.Tasks;
using Asp.Versioning;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.ViewModels.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Service.Controllers.V1
{
    /// <summary>
    /// Represents a <see cref="SecurityController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>    
    /// <param name="mediator">Injected <see cref="IMediator"/></param>
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{v:apiVersion}/security")]
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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("password/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] SecurityPasswordReset @viewModel) => Ok(value: await mediator.Send(new PasswordResetCommand { ViewModel = @viewModel }));

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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("password/change")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] SecurityPasswordChange @viewModel) => Ok(value:   await mediator.Send(new PasswordChangeCommand { ViewModel = @viewModel }));

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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("email/change")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] SecurityEmailChange @viewModel) => Ok(value: await mediator.Send(new EmailChangeCommand { ViewModel = @viewModel }));

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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("phonenumber/change")]
        [Authorize]
        public async Task<IActionResult> ChangePhoneNumber([FromBody] SecurityPhoneNumberChange @viewModel) => Ok(value:   await mediator.Send(new PhoneNumberChangeCommand { ViewModel = @viewModel }));

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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("name/change")]
        [Authorize]
        public async Task<IActionResult> ChangeName([FromBody] SecurityNameChange @viewModel) => Ok(value:   await mediator.Send(new NameChangeCommand { ViewModel = @viewModel }));

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
        [MapToApiVersion(1.0)]
        [HttpPut]
        [Route("tokens/refresh")]
        public async Task<IActionResult> RefreshTokens([FromBody] SecurityRefreshTokenReset @viewModel) => Ok(value:   await mediator.Send(new RefreshTokenResetCommand { ViewModel = @viewModel }));
    }
}
