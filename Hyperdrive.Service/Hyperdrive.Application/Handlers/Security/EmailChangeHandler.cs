using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.Security;

public class EmailChangeHandler : IRequestHandler<EmailChangeCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;   
    private readonly ISecurityManager _securityManager;
    
    public EmailChangeHandler(
        IApplicationUserManager userManager,      
        ISecurityManager securityManager)
    {
        _userManager = userManager;      
        _securityManager = securityManager;
    }
    
    public async Task<ViewApplicationUser> Handle(EmailChangeCommand request, CancellationToken cancellationToken)
    {
        await _userManager.CheckEmail(request.ViewModel.NewEmail, request.ViewModel.ApplicationUserId);
        
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);
        
        await _securityManager.ChangeEmail(@user, request.ViewModel.NewEmail);     
        
        var @dto  = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}