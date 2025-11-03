using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.Security;

public class PasswordChangeHandler : IRequestHandler<PasswordChangeCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;  
    private readonly ISecurityManager _securityManager;
    
    public PasswordChangeHandler(
        IApplicationUserManager userManager,      
        ISecurityManager securityManager)
    {
        _userManager = userManager;     
        _securityManager = securityManager;
    }
    
    public async Task<ViewApplicationUser> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);
        
        await _securityManager.ChangePassword(@user, request.ViewModel.CurrentPassword,request.ViewModel.NewPassword);       
             
        var @dto  = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}