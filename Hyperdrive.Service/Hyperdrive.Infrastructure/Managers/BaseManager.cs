using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.Options;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="BaseManager"/> class. Implements <see cref="IBaseManager"/>
    /// </summary>
    public class BaseManager : IBaseManager
    {
        /// <summary>
        /// Instance of <see cref="IApplicationContext"/>
        /// </summary>
        protected readonly IApplicationContext Context;
       
        /// <summary>
        /// Instance of <see cref="IOptions{JwtSettings}"/>
        /// </summary>
        protected readonly IOptions<JwtSettings> JwtSettings;

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseManager"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        public BaseManager(IApplicationContext @context)
        {
            Context = @context;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseManager"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseManager(IApplicationContext @context,
                           IOptions<JwtSettings> @jwtSettings)
        {
            Context = @context;
            JwtSettings = @jwtSettings;
        }      
    }
}
