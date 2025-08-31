using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationUser;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class UpdateApplicationUserHandler : IRequestHandler<UpdateApplicationUserCommand, ViewApplicationUser>
{
    private readonly IApplicationUserManager _manager;

    public UpdateApplicationUserHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }
    
    public async Task<ViewApplicationUser> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var @user = await _manager.UpdateApplicationUserRoles(
            request.ViewModel.ApplicationRoleNames,
            request.ViewModel.ApplicationUserId);

        return @user.ToViewModel();
    }
}