using System.Threading.Tasks;

using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Security;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hyperdrive.Tier.Web.Controllers
{
    /// <summary>
    /// Represents a <see cref="SecurityController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/security")]
    [Produces("application/json")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="ISecurityService"/>
        /// </summary>
        private readonly ISecurityService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityController"/>
        /// </summary>
        /// <param name="service">Injected <see cref="ISecurityService"/></param>
        public SecurityController(ISecurityService @service) => Service = @service;

        [HttpPut]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]SecurityPasswordReset @viewModel) => new JsonResult(value: await Service.ResetPassword(@viewModel));

        [HttpPut]
        [Route("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]SecurityPasswordChange @viewModel) => new JsonResult(value: await Service.ChangePassword(@viewModel));

        [HttpPut]
        [Route("changeemail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody]SecurityEmailChange @viewModel) => new JsonResult(value: await Service.ChangeEmail(@viewModel));
    }
}
