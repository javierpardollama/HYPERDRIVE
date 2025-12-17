using Asp.Versioning;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Service.Controllers.V1;

/// <summary>
/// Represents a <see cref="DriveItemVersionController"/> class. Inherits <see cref="ControllerBase"/>
/// </summary>   
/// <param name="mediator">Injected <see cref="IMediator"/></param>
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{v:apiVersion}/driveitem/version")]
[Produces("application/json")]
[Authorize]
[ApiController]
[EnableRateLimiting("Concurrency")]
public class DriveItemVersionController(IMediator @mediator) : ControllerBase
{
    /// <summary>
    /// Finds Paginated DriveItem Version By DriveItem Id
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="FilterPageDriveItemVersion"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewPage<ViewDriveItemVersion>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindPaginatedDriveItemVersionByDriveItemId([FromBody] FilterPageDriveItemVersion @viewModel) => Ok(value: await mediator.Send(new FindPaginatedDriveItemVersionByDriveItemIdQuery { ViewModel = viewModel }));


    /// <summary>
    /// Targets Drive Item Version By Id
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
    [HttpPost]
    [Route("target/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> TargetDriveItemVersionById(int @id)
    {
        await mediator.Send(new TargetDriveItemVersionByIdCommand { Id = @id });

        return Ok();
    }
}
