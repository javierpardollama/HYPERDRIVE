﻿using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace Hyperdrive.Service.Controllers
{

    /// <summary>
    /// Represents a <see cref="ApplicationUserController"/> class. Inherits <see cref="ControllerBase"/> 
    /// </summary>
    /// <param name="service">Injected <see cref="IApplicationUserService"/></param>
    [Route("api/applicationuser")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("Concurrency")]
    public class ApplicationUserController(IApplicationUserService @service) : ControllerBase
    {
        /// <summary>
        /// Updates Application User
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpPut]
        [Route("updateapplicationuser")]
        public async Task<IActionResult> UpdateApplicationUser([FromBody] UpdateApplicationUser @viewModel) => Ok(value: await @service.UpdateApplicationUser(@viewModel));

        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [Route("findallapplicationuser")]
        public async Task<IActionResult> FindAllApplicationUser() => Ok(value: await @service.FindAllApplicationUser());

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpPost]
        [Route("findpaginatedapplicationuser")]
        public async Task<IActionResult> FindPaginatedApplicationUser([FromBody] FilterPageApplicationUser @viewModel) => Ok(value: await @service.FindPaginatedApplicationUser(@viewModel));


        /// <summary>
        /// Removes Application User ById
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{OkObjectResult}"/></returns>
        [HttpDelete]
        [Route("removeapplicationuserbyid/{id}")]
        public async Task<IActionResult> RemoveApplicationUserById(int @id)
        {
            await @service.RemoveApplicationUserById(@id);

            return Ok();
        }
    }
}
