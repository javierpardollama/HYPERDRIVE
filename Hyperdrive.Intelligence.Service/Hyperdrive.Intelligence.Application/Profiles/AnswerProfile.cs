using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Dtos;

namespace Hyperdrive.Intelligence.Application.Profiles;

/// <summary>
/// Represents a <see cref="AnswerProfile"/> class.
/// </summary>
public static class AnswerProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="AnswerDto"/></param>
    /// <returns>Instance of <see cref="ViewAnswer"/></returns>
    public static ViewAnswer ToViewModel(this AnswerDto @dto)
    {
        return new ViewAnswer
        {
            Text = @dto.Text,
            LastModified = @dto.LastModified,
            Sources = [.. dto.Sources.Select(s => s.ToViewModel())]
        };
    }
}
