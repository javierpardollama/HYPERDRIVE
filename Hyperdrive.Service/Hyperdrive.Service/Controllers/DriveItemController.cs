using System.Net;
using System.Threading.Tasks;

using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hyperdrive.Tier.Web.Controllers
{
    /// <summary>
    /// Represents a <see cref="DriveItemController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>   
    /// <param name="service">Injected <see cref="IDriveItemService"/></param>
    [Route("api/driveitem")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    public class DriveItemController(IDriveItemService @service) : ControllerBase
    {
        /// <summary>
        /// Finds Paginated DriveItem By ApplicationUser Id
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageDriveItem"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPost]
        [Route("findpaginatedarchivebyapplicationuserid")]
        public async Task<IActionResult> FindPaginatedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => new JsonResult(value: await @service.FindPaginatedDriveItemByApplicationUserId(@viewModel));

        /// <summary>
        /// Finds Paginated Shared DriveItem By ApplicationUser Id
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageDriveItem"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPost]
        [Route("findpaginatedsharedarchivebyapplicationuserid")]
        public async Task<IActionResult> FindPaginatedSharedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => new JsonResult(value: await @service.FindPaginatedSharedDriveItemByApplicationUserId(@viewModel));

        /// <summary>
        /// Finds All DriveItem Version By DriveItem Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallarchiveversionbyarchiveid/{id}")]
        public async Task<IActionResult> FindAllDriveItemVersionByDriveItemId(int @id) => new JsonResult(value: await @service.FindAllDriveItemVersionByDriveItemId(@id));

        /// <summary>
        /// Finds All DriveItem
        /// </summary>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallarchive")]
        public async Task<IActionResult> FindAllDriveItem() => new JsonResult(value: await @service.FindAllDriveItem());

        /// <summary>
        /// Adds DriveItem
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddDriveItem"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPost]
        [Route("addarchive")]
        public async Task<IActionResult> AddDriveItem([FromBody]AddDriveItem @viewModel) => new JsonResult(value: await @service.AddDriveItem(@viewModel));

        /// <summary>
        /// Updates DriveItem
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateDriveItem"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("updatearchive")]
        public async Task<IActionResult> UpdateDriveItem([FromBody]UpdateDriveItem @viewModel) => new JsonResult(value: await @service.UpdateDriveItem(@viewModel));

        /// <summary>
        /// Removes DriveItem By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpDelete]
        [Route("removearchivebyid/{id}")]
        public async Task<IActionResult> RemoveDriveItemById(int @id)
        {
            await @service.RemoveDriveItemById(@id);

            return new JsonResult((int)HttpStatusCode.OK);
        }
    }
}
