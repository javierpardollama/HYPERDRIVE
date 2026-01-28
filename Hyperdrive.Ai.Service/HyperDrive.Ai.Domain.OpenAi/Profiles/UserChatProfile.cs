using Hyperdrive.Ai.Domain.Entities;
using OpenAI.Chat;

namespace Hyperdrive.Ai.Infrastructure.Profiles;

/// <summary>
/// Represents a <see cref="UserChatProfile"/> class.
/// </summary>
public static class UserChatProfile
{
    /// <summary>
    /// Transforms to Message
    /// </summary>
    /// <param name="entity">Injected <see cref="Query"/></param>
    /// <returns>Instance of <see cref="UserChatMessage"/></returns>
    public static UserChatMessage ToMessage(this Query @entity)
    {
        return new UserChatMessage(@entity.Content);
    }
}
