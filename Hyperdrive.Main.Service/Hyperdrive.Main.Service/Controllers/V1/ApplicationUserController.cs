using Asp.Versioning;
using Hyperdrive.Main.Application.Commands.ApplicationUser;
using Hyperdrive.Main.Application.Queries.ApplicationUser;
using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Service.Controllers.V1;


/// <summary>
/// Represents a <see cref="ApplicationUserController"/> class. Inherits <see cref="ControllerBase"/> 
/// </summary>
/// <param name="mediator">Injected <see cref="IMediator"/></param>
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{v:apiVersion}/applicationuser")]
[Produces("application/json")]
[ApiController]
[Authorize]
[EnableRateLimiting("Concurrency")]
public class ApplicationUserController(IMediator @mediator) : ControllerBase
{
    /// <summary>
    /// Updates Application User
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewApplicationUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateApplicationUser([FromBody] UpdateApplicationUser @viewModel) => Ok(value: await mediator.Send(new UpdateApplicationUserCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Finds All Application User
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="409">Conflict</response>
    /// <response code="401">Unauthorized</response>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    [Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ViewCatalog>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindAllApplicationUser() => Ok(value: await mediator.Send(new FindAllApplicationUserQuery()));


    /// <summary>
    /// Finds Paginated Application User
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="FilterPageApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewPage<ViewApplicationUser>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindPaginatedApplicationUser([FromBody] FilterPageApplicationUser @viewModel) => Ok(value: await mediator.Send(new FindPaginatedApplicationUserQuery { ViewModel = @viewModel }));

    /// <summary>
    /// Removes Application User ById
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="id">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpDelete]
    [Route("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RemoveApplicationUserById(int @id)
    {
        await mediator.Send(new RemoveApplicationUserByIdCommand { Id = @id });

        return Ok();
    }
}
