using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
/// Represents a <see cref="SourceProfile"/> class.
/// </summary>
public static class SourceProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="SourceDto"/></param>
    /// <returns>Instance of <see cref="ViewSource"/></returns>
    public static ViewSource ToViewModel(this SourceDto @dto)
    {
        return new ViewSource
        {
            DocumentId = @dto.DocumentId,
            Preview = @dto.Preview,
            LastModified = @dto.LastModified,
        };
    }
}
