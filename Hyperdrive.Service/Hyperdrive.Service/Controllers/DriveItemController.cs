using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Updates;
using MediatR;

namespace Hyperdrive.Service.Controllers
{
    /// <summary>
    /// Represents a <see cref="DriveItemController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>   
    /// <param name="mediator">Injected <see cref="IMediator"/></param>
    [Route("api/driveitem")]
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
        [HttpPost]
        [Route("findpaginateddriveitembyapplicationuserid")]
        public async Task<IActionResult> FindPaginatedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => Ok(value: await mediator.Send(new FindPaginatedDriveItemByApplicationUserIdQuery {ViewModel = @viewModel}));

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
        [HttpPost]
        [Route("findpaginatedshareddriveitembyapplicationuserid")]
        public async Task<IActionResult> FindPaginatedSharedDriveItemByApplicationUserId([FromBody] FilterPageDriveItem @viewModel) => Ok(value: await mediator.Send(new FindPaginatedSharedDriveItemByApplicationUserIdQuery {ViewModel = @viewModel}));

        /// <summary>
        /// Finds All DriveItem Version By DriveItem Id
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
        [HttpGet]
        [Route("findalldriveitemversionbydriveitemid/{id}")]
        public async Task<IActionResult> FindAllDriveItemVersionByDriveItemId(int @id) => Ok(value: await mediator.Send(new FindAllDriveItemVersionByDriveItemIdQuery {Id = @id}));
      
        /// <summary>
        /// Finds Drive Item Binary By DriveItem Id
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
        [HttpGet]
        [Route("finddriveitembinarybyid/{id}")]
        public async Task<IActionResult> FindDriveItemBinaryByIdQuery(int @id) => Ok(value: await mediator.Send(new FindDriveItemBinaryByIdQuery {Id = @id}));
        
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
        [HttpPost]
        [Route("adddriveitem")]
        public async Task<IActionResult> AddDriveItem([FromBody] AddDriveItem @viewModel) => Ok(value:await mediator.Send(new AddDriveItemCommand {ViewModel = @viewModel}));

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
        [HttpPut]
        [Route("updatedriveitemname")]
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
        [HttpPut]
        [Route("updatedriveitemsharedwith")]
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
        [HttpDelete]
        [Route("removedriveitembyid/{id}")]
        public async Task<IActionResult> RemoveDriveItemById(int @id)
        {
            await mediator.Send(new RemoveDriveItemByIdCommand { Id = @id });

            return Ok();
        }
    }
}
