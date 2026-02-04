using Hyperdrive.Main.Application.Commands.Security;
using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.Security;

public class RefreshTokenResetHandler : IRequestHandler<RefreshTokenResetCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;
    private readonly ITokenManager _tokenManager;
    private readonly IRefreshTokenManager _refreshTokenManager;

    public RefreshTokenResetHandler(
        IApplicationUserManager userManager,
        ITokenManager tokenManager,
        IRefreshTokenManager refreshTokenManager)
    {
        _userManager = userManager;
        _tokenManager = tokenManager;
        _refreshTokenManager = refreshTokenManager;
    }

    public async Task<ViewApplicationUser> Handle(RefreshTokenResetCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);

        await _refreshTokenManager.IsRevoked(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);

        await _refreshTokenManager.Revoke(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);

        await _tokenManager.AddApplicationUserToken(@user);

        await _refreshTokenManager.AddApplicationUserRefreshToken(@user);

        var @dto = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}