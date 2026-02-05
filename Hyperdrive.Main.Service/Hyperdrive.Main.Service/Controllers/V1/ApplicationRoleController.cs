using Asp.Versioning;
using Hyperdrive.Main.Application.Commands.ApplicationRole;
using Hyperdrive.Main.Application.Queries.ApplicationRole;
using Hyperdrive.Main.Application.ViewModels.Additions;
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
/// Represents a <see cref="ApplicationRoleController"/> class. Inherits <see cref="ControllerBase"/> 
/// </summary>
/// <param name="mediator">Injected <see cref="IMediator"/></param>
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{v:apiVersion}/applicationrole")]
[Produces("application/json")]
[ApiController]
[Authorize]
[EnableRateLimiting("Concurrency")]
public class ApplicationRoleController(IMediator @mediator) : ControllerBase
{
    /// <summary>
    /// Updates Application Role
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
    /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewApplicationRole))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateApplicationRole([FromBody] UpdateApplicationRole @viewModel) => Ok(value: await mediator.Send(new UpdateApplicationRoleCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Finds All Application Role
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
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindAllApplicationRole() => Ok(value: await mediator.Send(new FindAllApplicationRoleQuery()));

    /// <summary>
    /// Finds Paginated Application Role
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
    /// <param name="viewModel">Injected <see cref="FilterPageApplicationRole"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewPage<ViewApplicationRole>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindPaginatedApplicationRole([FromBody] FilterPageApplicationRole @viewModel) => Ok(value: await mediator.Send(new FindPaginatedApplicationRoleQuery { ViewModel = viewModel }));

    /// <summary>
    /// Adds Application Role
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
    /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewApplicationRole))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddApplicationRole([FromBody] AddApplicationRole @viewModel) => Ok(value: await mediator.Send(new AddApplicationRoleCommand { ViewModel = viewModel }));

    /// <summary>
    /// Removes Application Role ById
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
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RemoveApplicationRoleById(int @id)
    {
        await mediator.Send(new RemoveApplicationRoleByIdCommand { Id = @id });

        return Ok();
    }
}