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
        /// Instance of <see cref="IOptions{JwtSettings}"/>
        /// </summary>
        protected readonly IOptions<JwtSettings> JwtSettings;


        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        public BaseService(IApplicationContext @context,
                           IMapper @mapper)
        {
            Context = @context;
            Mapper = @mapper;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseService(IApplicationContext @context,
                           IMapper @mapper,
                           IOptions<JwtSettings> @jwtSettings)
        {
            Context = @context;
            Mapper = @mapper;
            JwtSettings = @jwtSettings;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        public BaseService(IMapper @mapper)
        {
            Mapper = @mapper;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="logger">Injected <see cref="ILogger"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseService(IOptions<JwtSettings> @jwtSettings)
        {
            JwtSettings = @jwtSettings;
        }

        /// <summary>
        /// Initializes a new Instance of <see cref="BaseService"/>
        /// </summary>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public BaseService(IApplicationContext @context,
                           IOptions<JwtSettings> @jwtSettings)
        {
            Context = @context;
            JwtSettings = @jwtSettings;
        }
    }
}
