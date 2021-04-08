using AutoMapper;

using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="BaseService"/> class. Implements <see cref="IBaseService"/>
    /// </summary>
    public class BaseService : IBaseService
    {
        /// <summary>
        /// Instance of <see cref="IApplicationContext"/>
        /// </summary>
        protected readonly IApplicationContext Context;

        /// <summary>
        /// Instance of <see cref="IMapper"/>
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// Instance of <see cref="ILogger"/>
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Instance of <see cref="IOptions{JwtSettings}"/>
        /// </summary>
        protected readonly IOptions<JwtSettings> JwtSettings;


        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        /// <param name="logger">Injected <see cref="ILogger"/></param>
        public BaseService(IApplicationContext @context,
                           IMapper @mapper,
                           ILogger @logger)
        {
            Context = @context;
            Mapper = @mapper;
            Logger = @logger;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        /// <param name="logger">Injected <see cref="ILogger"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseService(IMapper @mapper,
                           ILogger @logger,
                           IOptions<JwtSettings> @jwtSettings)
        {
            Mapper = @mapper;
            Logger = @logger;
            JwtSettings = @jwtSettings;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="logger">Injected <see cref="ILogger"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseService(
            ILogger @logger,
            IOptions<JwtSettings> @jwtSettings
           )
        {
            Logger = @logger;
            JwtSettings = @jwtSettings;
        }
    }
}
