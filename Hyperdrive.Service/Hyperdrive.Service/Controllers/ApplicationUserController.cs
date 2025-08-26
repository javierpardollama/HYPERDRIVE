using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationUser;
using Hyperdrive.Application.Queries.ApplicationUser;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Updates;
using MediatR;

namespace Hyperdrive.Service.Controllers
{

    /// <summary>
    /// Represents a <see cref="ApplicationUserController"/> class. Inherits <see cref="ControllerBase"/> 
    /// </summary>
    /// <param name="mediator">Injected <see cref="IMediator"/></param>
    [Route("api/applicationuser")]
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
        [HttpPut]
        [Route("updateapplicationuser")]
        public async Task<IActionResult> UpdateApplicationUser([FromBody] UpdateApplicationUser @viewModel) => Ok(value: await mediator.Send(new UpdateApplicationUserCommand {ViewModel = @viewModel}));

        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [Route("findallapplicationuser")]
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
        [HttpPost]
        [Route("findpaginatedapplicationuser")]
        public async Task<IActionResult> FindPaginatedApplicationUser([FromBody] FilterPageApplicationUser @viewModel) => Ok(value: await mediator.Send(new FindPaginatedApplicationUserQuery { ViewModel = @viewModel}));

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
        [HttpDelete]
        [Route("removeapplicationuserbyid/{id}")]
        public async Task<IActionResult> RemoveApplicationUserById(int @id)
        {
            await mediator.Send(new RemoveApplicationUserByIdCommand { Id = @id });

            return Ok();
        }
    }
}
