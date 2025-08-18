using Hyperdrive.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Application.Installers;

/// <summary>
///     Represents a <see cref="ProblemDetailsInstaller" /> class.
/// </summary>
public static class ProblemDetailsInstaller
{
    /// <summary>
    ///     Installs Problem Details
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallProblemDetails(this IServiceCollection @this)
    {
        // Return the Problem Details format for non-successful responses
        @this.AddProblemDetails();
        @this.AddExceptionHandler<ProblemDetailsExceptionHandler>();
    }
}