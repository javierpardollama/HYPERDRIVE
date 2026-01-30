using Asp.Versioning;
using Hyperdrive.Ai.Application.Commands.Chats;
using Hyperdrive.Ai.Application.ViewModels.Additions;
using Hyperdrive.Ai.Application.ViewModels.Removes;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Service.Controllers.V1;

/// <summary>
/// Represents a <see cref="ChatController"/> class. Inherits <see cref="ControllerBase"/>
/// </summary>   
/// <param name="mediator">Injected <see cref="IMediator"/></param>
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{v:apiVersion}/chat")]
[Produces("application/json")]
[ApiController]
[EnableRateLimiting("Concurrency")]
public class ChatController(IMediator @mediator) : ControllerBase
{
    /// <summary>
    ///     Removes Chat By Id
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>
    /// <param name="viewModel">Injected <see cref="ViewRemoveChat"/></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult}" /></returns>
    [MapToApiVersion(1.0)]
    [HttpPut]
    [Route("remove")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RemoveChatById([FromBody] ViewRemoveChat @viewModel)
    {
        await mediator.Send(new RemoveChatCommand { ViewModel = @viewModel });

        return Ok();
    }

    /// <summary>
    ///     Adds Chat
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="408">RequestTimeout</response>
    /// <response code="404">NotFound</response>
    /// <response code="409">Conflict</response>
    /// <response code="503">ServiceUnavailable</response>
    /// <response code="500">InternalServerError</response>
    /// <param name="viewModel">Injected <see cref="ViewAddChat" /></param>
    /// <returns>Instance of <see cref="Task{OkObjectResult{ViewChat}}" /></returns>
    [MapToApiVersion(1.0)]
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewChat))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddChat([FromBody] ViewAddChat viewModel)
    {
        return Ok(await mediator.Send(new AddChatCommand { ViewModel = viewModel }));
    }
}
