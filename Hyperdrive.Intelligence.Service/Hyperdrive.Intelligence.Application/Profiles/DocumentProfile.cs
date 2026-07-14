using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Dtos;

namespace Hyperdrive.Intelligence.Application.Profiles;

/// <summary>
///     Represents a <see cref="DocumentProfile" /> class
/// </summary>
public static class DocumentProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="DocumentDto"/></param>
    /// <returns>Instance of <see cref="ChatDto"/></returns>
    public static ViewDocument ToViewModel(this DocumentDto @dto)
    {
        return new ViewDocument
        {
            Id = @dto.Id,
            LastModified = @dto.LastModified,
            Name = @dto.Name
        };
    }
}