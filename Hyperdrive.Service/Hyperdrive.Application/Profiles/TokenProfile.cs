using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

public static class TokenProfile
{
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