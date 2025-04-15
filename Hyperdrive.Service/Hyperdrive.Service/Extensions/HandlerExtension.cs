using Hyperdrive.Tier.Handlers.Classes;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="HandlerExtension"/> class.
    /// </summary>
    public static class HandlerExtension
    {
        /// <summary>
        /// Extends Customized Handlers
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>
        public static void AddCustomizedHandlers(this IServiceCollection @this)
        {
            @this.AddExceptionHandler<ProblemDetailsExceptionHandler>();
        }

    }
}
