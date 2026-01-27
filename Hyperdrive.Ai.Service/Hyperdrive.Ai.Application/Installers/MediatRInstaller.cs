using Hyperdrive.Ai.Application.Handlers.Chats;
using Hyperdrive.Ai.Application.Handlers.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Ai.Application.Installers;

/// <summary>
///     Represents a <see cref="InstallMediatR" /> class.
/// </summary>
public static class MediatRInstaller
{
    /// <summary>
    ///     Installs MediatR
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallMediatR(this IServiceCollection @this)
    {
        @this.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AddDocumentHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AddChatMessageHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveDocumentHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveChatHandler>();
        });
    }
}