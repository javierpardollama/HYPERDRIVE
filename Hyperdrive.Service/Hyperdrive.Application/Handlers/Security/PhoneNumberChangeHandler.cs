using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.Security;

public class PhoneNumberChangeHandler : IRequestHandler<PhoneNumberChangeCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _userManager;
 
    private readonly ISecurityManager _securityManager;
    
    public PhoneNumberChangeHandler(
        IApplicationUserManager userManager,     
        ISecurityManager securityManager)
    {
        _userManager = userManager;      
        _securityManager = securityManager;
    }
    
    public async Task<ViewApplicationUser> Handle(PhoneNumberChangeCommand request, CancellationToken cancellationToken)
    {
        var @user = await _userManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);
        
        await _securityManager.ChangePhoneNumber(@user, request.ViewModel.NewPhoneNumber);    
        
        var @dto  = await _userManager.ReloadApplicationUserById(@user.Id);

        return @dto.ToViewModel();
    }
}