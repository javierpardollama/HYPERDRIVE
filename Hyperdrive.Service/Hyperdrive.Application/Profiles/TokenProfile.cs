using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="TokenProfile"/> class.
/// </summary>
public static class TokenProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="TokenDto"/></param>
    /// <returns>Instance of <see cref="ViewToken"/></returns>
    public static ViewToken ToViewModel(this TokenDto @dto)
    {
        return new ViewToken
        {
            IssuedAt = @dto.IssuedAt,
            LoginProvider = @dto.LoginProvider,
            Value = @dto.Value
        };
    }
}