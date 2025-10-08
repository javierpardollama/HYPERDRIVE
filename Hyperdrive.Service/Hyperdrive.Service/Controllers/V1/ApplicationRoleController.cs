using System.Threading.Tasks;
using Asp.Versioning;
using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Application.Queries.ApplicationRole;
using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Updates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Service.Controllers.V1
{
    /// <summary>
    /// Represents a <see cref="ApplicationRoleController"/> class. Inherits <see cref="ControllerBase"/> 
    /// </summary>
    /// <param name="mediator">Injected <see cref="IMediator"/></param>
    [ApiVersion(1)]
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
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [MapToApiVersion(1)]
        [HttpPut]
        [Route("updateapplicationrole")]
        public async Task<IActionResult> UpdateApplicationRole([FromBody] UpdateApplicationRole @viewModel) => Ok(value: await mediator.Send(new UpdateApplicationRoleCommand {ViewModel = @viewModel}));

        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="408">RequestTimeout</response>
        /// <response code="404">NotFound</response>
        /// <response code="409">Conflict</response>
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [MapToApiVersion(1)]
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [Route("findallapplicationrole")]
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
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [MapToApiVersion(1)]
        [HttpPost]
        [Route("findpaginatedapplicationrole")]
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
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [MapToApiVersion(1)]
        [HttpPost]
        [Route("addapplicationrole")]
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
        /// <response code="503">ServiceUnavailable</response>
        /// <response code="500">InternalServerError</response>     
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [MapToApiVersion(1)]
        [HttpDelete]
        [Route("removeapplicationrolebyid/{id}")]
        public async Task<IActionResult> RemoveApplicationRoleById(int @id)
        {
            await mediator.Send(new RemoveApplicationRoleByIdCommand { Id = @id });

            return Ok();
        }
    }
}