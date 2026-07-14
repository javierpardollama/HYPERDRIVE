using Hyperdrive.Identity.Application.Commands.ApplicationUser;
using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationUser;

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