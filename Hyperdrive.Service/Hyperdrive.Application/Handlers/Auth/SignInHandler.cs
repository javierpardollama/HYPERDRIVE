using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Auth;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.Auth;

public class SignInHandler : IRequestHandler<SignInCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;
    private readonly IAuthManager _authManager;
    private readonly ITokenManager _tokenManager;
    private readonly IRefreshTokenManager _refreshTokenManager;
    
    public SignInHandler(
        IApplicationUserManager userManager, 
        IAuthManager authManager, 
        ITokenManager tokenManager, 
        IRefreshTokenManager refreshTokenManager)
    {
        _userManager = userManager;
        _authManager = authManager;
        _tokenManager = tokenManager;
        _refreshTokenManager = refreshTokenManager;
    }
    
    public async Task<ViewApplicationUser> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserByEmail(request.ViewModel.Email);
        
        await _authManager.SignIn(@user, request.ViewModel.Email, request.ViewModel.Password);

        await _tokenManager.AddApplicationUserToken(user);

        await _refreshTokenManager.AddApplicationUserRefreshToken(user);
        
        var dto  = await _userManager.ReloadApplicationUserById(user.Id);

        return dto.ToViewModel();
    }
}