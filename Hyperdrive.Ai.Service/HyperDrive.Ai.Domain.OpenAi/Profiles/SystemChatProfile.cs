using Hyperdrive.Ai.Domain.Entities;
using OpenAI.Chat;

namespace Hyperdrive.Ai.Infrastructure.Profiles;

/// <summary>
/// Represents a <see cref="SystemChatProfile"/> class.
/// </summary>
public static class SystemChatProfile
{
    /// <summary>
    /// Transforms to Message
    /// </summary>
    /// <param name="entity">Injected <see cref="Arrange"/></param>
    /// <returns>Instance of <see cref="UserChatMessage"/></returns>
    public static SystemChatMessage ToMessage(this Arrange @entity)
    {
        return new SystemChatMessage(@entity.Content);
    }
}
