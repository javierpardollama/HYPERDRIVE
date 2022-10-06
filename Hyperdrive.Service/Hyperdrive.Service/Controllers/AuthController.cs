using System.Threading.Tasks;

using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Auth;

using Microsoft.AspNetCore.Mvc;

namespace Hyperdrive.Tier.Web.Controllers
{
    /// <summary>
    /// Represents a <see cref="AuthController"/> class. Inherits <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IArchiveService"/>
        /// </summary>
        private readonly IAuthService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="AuthController"/>
        /// </summary>
        /// <param name="service">Injected <see cref="IAuthService"/></param>
        public AuthController(IAuthService @service) => Service = @service;

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody]AuthSignIn @viewModel) => new JsonResult(value: await Service.SignIn(@viewModel));

        [HttpPost]
        [Route("joinin")]
        public async Task<IActionResult> JoinIn([FromBody]AuthJoinIn @viewModel) => new JsonResult(value: await Service.JoinIn(@viewModel));
    }
}
