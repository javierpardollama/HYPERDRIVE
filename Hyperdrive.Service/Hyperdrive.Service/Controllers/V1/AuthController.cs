using System.Threading.Tasks;
using Asp.Versioning;
using Hyperdrive.Application.Commands.Auth;
using Hyperdrive.Application.ViewModels.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Service.Controllers.V1
{
    /// <summary>
    /// Represents a <see cref="AuthController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>   
    //// <param name="mediator">Injected <see cref="IMediator"/></param>
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{v:apiVersion}/auth")]
    [Produces("application/json")]
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    public class AuthController(IMediator @mediator) : ControllerBase
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
        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthSignIn @viewModel) => Ok(value: await mediator.Send(new SignInCommand {ViewModel = @viewModel}));

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
        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> JoinIn([FromBody] AuthJoinIn @viewModel) => Ok(value: await mediator.Send(new JoinInCommand {ViewModel = @viewModel}));

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
        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("out")]
        [Authorize]
        public async Task<IActionResult> SignOut([FromBody] AuthSignOut @viewModel)
        {
            await mediator.Send(new SignOutCommand { ViewModel = @viewModel });

            return Ok();
        }
    }
}
