using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class NameChangeHandler : IRequestHandler<NameChangeCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;
    private readonly ITokenManager _tokenManager;
    private readonly IRefreshTokenManager _refreshTokenManager;
    private readonly ISecurityManager _securityManager;
    
    public NameChangeHandler(
        IApplicationUserManager userManager, 
        ITokenManager tokenManager, 
        IRefreshTokenManager refreshTokenManager,
        ISecurityManager securityManager)
    {
        _userManager = userManager;
        _tokenManager = tokenManager;
        _refreshTokenManager = refreshTokenManager;
        _securityManager = securityManager;
    }
    
    public async Task<ViewApplicationUser> Handle(NameChangeCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);
        
        await _securityManager.ChangeName(@user, request.ViewModel.NewFirstName, request.ViewModel.NewLastName);
        
        await _refreshTokenManager.IsRevoked(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);
        
        await _refreshTokenManager.Revoke(request.ViewModel.ApplicationUserId, request.ViewModel.ApplicationUserRefreshToken);

        await _tokenManager.AddApplicationUserToken(@user);

        await _refreshTokenManager.AddApplicationUserRefreshToken(@user);
        
        var @dto  = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}