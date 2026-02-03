using Hyperdrive.Ai.Application.Handlers.Chats;
using Hyperdrive.Ai.Application.Handlers.Documents;
using Hyperdrive.Ai.Application.Handlers.Interactions;
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
            cfg.RegisterServicesFromAssemblyContaining<RemoveDocumentHandler>();

            cfg.RegisterServicesFromAssemblyContaining<AddChatHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateChatTitleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveChatHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedChatHandler>();

            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedInteractionHandler>();

            cfg.RegisterServicesFromAssemblyContaining<AddInteractionHandler>();
        });
    }
}