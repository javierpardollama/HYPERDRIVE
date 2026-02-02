using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
/// Represents a <see cref="InteractionProfile"/> class.
/// </summary>
public static class InteractionProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="InteractionDto"/></param>
    /// <returns>Instance of <see cref="ViewInteraction"/></returns>
    public static ViewInteraction ToViewModel(this InteractionDto @dto)
    {
        return new ViewInteraction
        {
            LastModified = @dto.LastModified,
            Query = dto.Query?.ToViewModel(),
            Answer = dto.Answer?.ToViewModel()
        };
    }

    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{InteractionDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewInteraction}"/></returns>
    public static ViewPage<ViewInteraction> ToPageViewModel(this PageDto<InteractionDto> @dto)
    {
        return new ViewPage<ViewInteraction>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x => x.ToViewModel())]
        };
    }
}
