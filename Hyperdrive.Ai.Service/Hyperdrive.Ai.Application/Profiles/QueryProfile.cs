using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
/// Represents a <see cref="QueryProfile"/> class.
/// </summary>
public static class QueryProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="QueryDto"/></param>
    /// <returns>Instance of <see cref="ViewQuery"/></returns>
    public static ViewQuery ToViewModel(this QueryDto @dto)
    {
        return new ViewQuery
        {
            Text = @dto.Text,
            LastModified = @dto.LastModified
        };
    }
}
