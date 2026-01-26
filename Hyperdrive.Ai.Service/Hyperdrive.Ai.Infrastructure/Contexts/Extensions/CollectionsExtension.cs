using Hyperdrive.Ai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Hyperdrive.Ai.Infrastructure.Contexts.Extensions
{
    /// <summary>
    ///     Represents a <see cref="CollectionsExtension" /> class.
    /// </summary>
    public static class CollectionsExtension
    {
        /// <summary>
        ///     Extends Customized Collections
        /// </summary>
        /// <param name="this">Injected <see cref="ModelBuilder" /></param>
        public static void AddCustomizedCollections(this ModelBuilder @this)
        {
            // Configure entity filters           
            @this.Entity<Document>().ToCollection("documents");            
            @this.Entity<Chunk>().ToCollection("chunks");
            @this.Entity<Chat>().ToCollection("chats");
            @this.Entity<Interaction>().ToCollection("messages");
        }
    }
}
