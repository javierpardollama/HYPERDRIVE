using System.Net;
using System.Threading.Tasks;

using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Updates;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hyperdrive.Tier.Web.Controllers
{
    /// <summary>
    /// Represents a <see cref="ArchiveController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/archive")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IArchiveService"/>
        /// </summary>
        private readonly IArchiveService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="ArchiveController"/>
        /// </summary>
        /// <param name="service">Injected <see cref="IArchiveService"/></param>
        public ArchiveController(IArchiveService @service) => Service = @service;

        /// <summary>
        /// Finds All Archive By ApplicationUser Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallarchivebyapplicationuserid/{id}")]
        public async Task<IActionResult> FindAllArchiveByApplicationUserId(int @id) => new JsonResult(value: await Service.FindAllArchiveByApplicationUserId(@id));

        /// <summary>
        /// Finds All Shared Archive By ApplicationUser Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallsharedarchivebyapplicationuserid/{id}")]
        public async Task<IActionResult> FindAllSharedArchiveByApplicationUserId(int @id) => new JsonResult(value: await Service.FindAllSharedArchiveByApplicationUserId(@id));

        /// <summary>
        /// Finds All Archive Version By Archive Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallarchiveversionbyarchiveid/{id}")]
        public async Task<IActionResult> FindAllArchiveVersionByArchiveId(int @id) => new JsonResult(value: await Service.FindAllArchiveVersionByArchiveId(@id));

        /// <summary>
        /// Finds All Archive
        /// </summary>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpGet]
        [Route("findallarchive")]
        public async Task<IActionResult> FindAllArchive() => new JsonResult(value: await Service.FindAllArchive());

        /// <summary>
        /// Adds Archive
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddArchive"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPost]
        [Route("addarchive")]
        public async Task<IActionResult> AddArchive([FromBody]AddArchive @viewModel) => new JsonResult(value: await Service.AddArchive(@viewModel));

        /// <summary>
        /// Updates Archive
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateArchive"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpPut]
        [Route("updatearchive")]
        public async Task<IActionResult> UpdateArchive([FromBody]UpdateArchive @viewModel) => new JsonResult(value: await Service.UpdateArchive(@viewModel));

        /// <summary>
        /// Removes Archive By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{JsonResult}"/></returns>   
        [HttpDelete]
        [Route("removearchivebyid/{id}")]
        public async Task<IActionResult> RemoveArchiveById(int @id)
        {
            await Service.RemoveArchiveById(@id);

            return new JsonResult((int)HttpStatusCode.OK);
        }
    }
}
