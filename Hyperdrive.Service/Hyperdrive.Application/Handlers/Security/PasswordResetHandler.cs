using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class PasswordResetHandler : IRequestHandler<PasswordResetCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;
    private readonly ITokenManager _tokenManager;
    private readonly IRefreshTokenManager _refreshTokenManager;
    private readonly ISecurityManager _securityManager;
    
    public PasswordResetHandler(
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
    
    public async Task<ViewApplicationUser> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        await _userManager.CheckEmail(request.ViewModel.Email);
        
        var @user = await _userManager.FindApplicationUserByEmail(request.ViewModel.Email);

        await _securityManager.ResetPassword(@user, request.ViewModel.NewPassword);
        
        await _tokenManager.AddApplicationUserToken(@user);

        await _refreshTokenManager.AddApplicationUserRefreshToken(@user);
        
        var @dto  = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}