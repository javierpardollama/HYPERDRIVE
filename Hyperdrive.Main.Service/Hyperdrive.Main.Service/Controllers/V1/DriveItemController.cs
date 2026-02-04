using Asp.Versioning;
using Hyperdrive.Main.Application.Commands.DriveItem;
using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Main.Application.ViewModels.Additions;
using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Service.Controllers.V1;

/// <summary>
/// Represents a <see cref="DriveItemController"/> class. Inherits <see cref="ControllerBase"/>
/// </summary>   
/// <param name="mediator">Injected <see cref="IMediator"/></param>
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{v:apiVersion}/driveitem")]
[Produces("application/json")]
[Authorize]
[ApiController]
[EnableRateLimiting("Concurrency")]
public class DriveItemController(IMediator @mediator) : ControllerBase
{
    /// <summary>
    /// Finds Paginated DriveItem By ApplicationUser Id
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="FilterPageDriveItem"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewPage<ViewDriveItem>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindPaginatedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => Ok(value: await mediator.Send(new FindPaginatedDriveItemByApplicationUserIdQuery { ViewModel = @viewModel }));

    /// <summary>
    /// Finds Paginated Shared DriveItem By ApplicationUser Id
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="FilterPageDriveItem"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("page/shared")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewPage<ViewDriveItem>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> FindPaginatedSharedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => Ok(value: await mediator.Send(new FindPaginatedSharedDriveItemByApplicationUserIdQuery { ViewModel = @viewModel }));

    /// <summary>
    /// Adds DriveItem
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="AddDriveItem"/></param>
    /// <returns>Instance of <see cref="Task{JsonReOkObjectResultsult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("up")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewDriveItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddDriveItem([FromBody] AddDriveItem @viewModel) => Ok(value: await mediator.Send(new AddDriveItemCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Updates Drive Item Name
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="UpdateDriveItemName"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPut]
    [Route("name/change")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewDriveItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateDriveItemName([FromBody] UpdateDriveItemName @viewModel) => Ok(value: await mediator.Send(new UpdateDriveItemNameCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Updates Drive Item Shared With
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>     
    /// <param name="viewModel">Injected <see cref="UpdateDriveItemName"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>   
    [MapToApiVersion(1.0)]
    [HttpPut]
    [Route("share")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewDriveItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateDriveItemSharedWith([FromBody] UpdateDriveItemSharedWith @viewModel) => Ok(value: await mediator.Send(new UpdateDriveItemSharedWithCommand { ViewModel = @viewModel }));

    /// <summary>
    /// Removes Drive Item By Id
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
    public async Task<IActionResult> RemoveDriveItemById(int @id)
    {
        await mediator.Send(new RemoveDriveItemByIdCommand { Id = @id });

        return Ok();
    }
}
