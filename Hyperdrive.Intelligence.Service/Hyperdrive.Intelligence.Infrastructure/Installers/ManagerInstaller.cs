using Hyperdrive.Intelligence.Domain.Managers;
using Hyperdrive.Intelligence.Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Intelligence.Infrastructure.Installers;

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
        @this.AddTransient<IDocumentManager, DocumentManager>();
        @this.AddTransient<IEmbeddingManager, EmbeddingManager>();
        @this.AddTransient<IChunkManager, ChunkManager>();
        @this.AddTransient<IChatCompletitionManager, ChatCompletitionManager>();
        @this.AddTransient<IChatMessageManager, ChatMessageManager>();
        @this.AddTransient<IChatSummaryManager, ChatSummaryManager>();
        @this.AddTransient<IChatTitleManager, ChatTitleManager>();
        @this.AddTransient<IInteractionManager, InteractionManager>();
        @this.AddTransient<IChatManager, ChatManager>();
        // Add other managers here
    }
}