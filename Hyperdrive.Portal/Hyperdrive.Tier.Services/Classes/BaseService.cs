using AutoMapper;

using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    public class BaseService : IBaseService
    {
        protected readonly IApplicationContext Context;

        protected readonly IMapper Mapper;

        protected readonly ILogger Logger;

        protected readonly IConfiguration Configuration;

        protected readonly JwtSettings JwtSettings;

        public BaseService(IApplicationContext context,
                           IMapper mapper,
                           ILogger logger)
        {
            Context = context;
            Mapper = mapper;
            Logger = logger;
        }

        public BaseService(IMapper mapper,
                           ILogger logger)
        {
            Mapper = mapper;
            Logger = logger;
        }

        public BaseService(
            IConfiguration configuration
           )
        {
            Configuration = configuration;

            JwtSettings = new JwtSettings();
            Configuration.GetSection("Jwt").Bind(JwtSettings);
        }
    }
}
