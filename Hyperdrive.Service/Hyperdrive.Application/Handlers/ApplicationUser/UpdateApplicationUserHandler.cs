using Hyperdrive.Application.Commands.ApplicationUser;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        var @user = await _manager.FindApplicationUserById(request.ViewModel.ApplicationUserId);

        var @dto = await _manager.UpdateApplicationUserRoles(
            request.ViewModel.ApplicationRoleNames,
            @user);

        return @dto.ToViewModel();
    }
}