using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ManagerInstaller" /> class.
/// </summary>
public static class ManagerInstaller
{
    /// <summary>
    ///     Installs Managers
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallManagers(this IServiceCollection @this)
    {
        @this.AddTransient<IAuthManager, CredentialManager>();
        @this.AddTransient<IDocumentManager, DocumentManager>();
        @this.AddTransient<IEmbeddingManager, EmbeddingManager>();
        @this.AddTransient<IChunkManager, ChunkManager>();
        @this.AddTransient<IChatCompletitionManager, ChatCompletitionManager>();
        @this.AddTransient<IChatMessageManager, ChatMessageManager>();
        @this.AddTransient<IChatSummaryManager, ChatSummaryManager>();
        @this.AddTransient<IInteractionManager, InteractionManager>();
        @this.AddTransient<IChatManager, ChatManager>();
        // Add other managers here
    }
}