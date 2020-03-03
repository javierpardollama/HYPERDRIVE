using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Contexts.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Web.Extensions
{
    public static class ContextsExtension
    {
        public static void AddCustomizedContexts(this IServiceCollection @this)
        {
            @this.AddScoped<IApplicationContext, ApplicationContext>();

            // Add other services here
        }
    }
}
