using Asp.Versioning;
using Hyperdrive.Main.Application.Commands.Auth;
using Hyperdrive.Main.Application.ViewModels.Auth;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Service.Controllers.V1;

/// <summary>
/// Represents a <see cref="AuthController"/> class. Inherits <see cref="ControllerBase"/>
/// </summary>   
/// <param name="mediator">Injected <see cref="IMediator"/></param>
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
    /// <response code="424">FailedDependency</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>       
    /// <param name="viewModel">Injected <see cref="AuthSignIn"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("in")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewApplicationUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> SignIn([FromBody] AuthSignIn @viewModel) => Ok(value: await mediator.Send(new SignInCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Joins In
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="424">FailedDependency</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>      
    /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewApplicationUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> JoinIn([FromBody] AuthJoinIn @viewModel) => Ok(value: await mediator.Send(new JoinInCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Signs Out
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="424">FailedDependency</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="AuthSignOut"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("out")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> SignOut([FromBody] AuthSignOut @viewModel)
    {
        await mediator.Send(new SignOutCommand { ViewModel = @viewModel });

        return Ok();
    }
}
