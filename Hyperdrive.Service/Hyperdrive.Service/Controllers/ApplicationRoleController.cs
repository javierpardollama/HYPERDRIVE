using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Service.Controllers
{
    /// <summary>
    /// Represents a <see cref="ApplicationRoleController"/> class. Inherits <see cref="ControllerBase"/> 
    /// </summary>
    /// <param name="service">Injected <see cref="IApplicationRoleService"/></param>
    [Route("api/applicationrole")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("Concurrency")]
    public class ApplicationRoleController(IApplicationRoleService @service) : ControllerBase
    {
        /// <summary>
        /// Updates Application Role
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpPut]
        [Route("updateapplicationrole")]
        public async Task<IActionResult> UpdateApplicationRole([FromBody] UpdateApplicationRole @viewModel) => Ok(value: await @service.UpdateApplicationRole(@viewModel));

        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [Route("findallapplicationrole")]
        public async Task<IActionResult> FindAllApplicationRole() => Ok(value: await @service.FindAllApplicationRole());

        /// <summary>
        /// Finds Paginated Application Role
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpPost]
        [Route("findpaginatedapplicationrole")]
        public async Task<IActionResult> FindPaginatedApplicationRole([FromBody] FilterPageApplicationRole @viewModel) => Ok(value: await @service.FindPaginatedApplicationRole(@viewModel));

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpPost]
        [Route("addapplicationrole")]
        public async Task<IActionResult> AddApplicationRole([FromBody] AddApplicationRole @viewModel) => Ok(value: await @service.AddApplicationRole(@viewModel));

        /// <summary>
        /// Removes Application Role ById
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="409">Conflict</response>
        /// <response code="401">Unauthorized</response>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpDelete]
        [Route("removeapplicationrolebyid/{id}")]
        public async Task<IActionResult> RemoveApplicationRoleById(int @id)
        {
            await @service.RemoveApplicationRoleById(@id);

            return Ok();
        }
    }
}