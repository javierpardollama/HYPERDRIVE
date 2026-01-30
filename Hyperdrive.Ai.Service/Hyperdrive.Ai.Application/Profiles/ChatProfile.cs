using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
/// Represents a <see cref="ChatProfile"/> class.
/// </summary>
public static class ChatProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="ChatDto"/></param>
    /// <returns>Instance of <see cref="ViewChat"/></returns>
    public static ViewChat ToViewModel(this ChatDto @dto)
    {
        return new ViewChat
        {
            LastModified = @dto.LastModified,
            Title = @dto.Title
        };
    }
}
