using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;
using System.Linq;

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

    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{ChatDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewChat}"/></returns>
    public static ViewPage<ViewChat> ToPageViewModel(this PageDto<ChatDto> @dto)
    {
        return new ViewPage<ViewChat>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x => x.ToViewModel())]
        };
    }
}
