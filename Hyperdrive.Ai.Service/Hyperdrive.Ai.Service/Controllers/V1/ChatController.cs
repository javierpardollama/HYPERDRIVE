using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Ai.Service.Controllers.V1;

/// <summary>
/// Represents a <see cref="AuthController"/> class. Inherits <see cref="ControllerBase"/>
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
}
