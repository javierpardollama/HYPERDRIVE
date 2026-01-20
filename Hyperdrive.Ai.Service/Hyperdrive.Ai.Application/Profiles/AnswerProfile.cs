using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
/// Represents a <see cref="AnswerProfile"/> class.
/// </summary>
public static class AnswerProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="@dto">Injected <see cref="RagAnswerDto"/></param>
    /// <returns>Instance of <see cref="ViewAnswer"/></returns>
    public static ViewAnswer ToViewModel(this RagAnswerDto @dto)
    {
        return new ViewAnswer
        {
            Answer = dto.Answer,
            Sources = [.. dto.Sources.Select(s => s.ToViewModel())]
        };
    }
}
