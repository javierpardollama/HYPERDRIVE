using Hyperdrive.Ai.Domain.Entities;
using OpenAI.Chat;

namespace Hyperdrive.Ai.Infrastructure.Profiles;

/// <summary>
/// Represents a <see cref="AssistantChatProfile"/> class.
/// </summary>
public static class AssistantChatProfile
{
    /// <summary>
    /// Transforms to Message
    /// </summary>
    /// <param name="entity">Injected <see cref="Answer"/></param>
    /// <returns>Instance of <see cref="UserChatMessage"/></returns>
    public static AssistantChatMessage ToMessage(this Answer @entity)
    {
        return new AssistantChatMessage(@entity.Content);
    }
}
