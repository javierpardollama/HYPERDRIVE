using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Auth;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.Auth;

public class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IApplicationUserManager _userManager;
    private readonly IAuthManager _authManager;
    private readonly IRefreshTokenManager _refreshTokenManager;
    
    public SignOutHandler(
        IApplicationUserManager userManager, 
        IAuthManager authManager, 
        ITokenManager tokenManager, 
        IRefreshTokenManager refreshTokenManager)
    {
        _userManager = userManager;
        _authManager = authManager;
        _refreshTokenManager = refreshTokenManager;
    }
    
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);

        await _refreshTokenManager.IsRevoked(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);
        
        await _refreshTokenManager.Revoke(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);
        
        await _authManager.SignOut();
    }
}