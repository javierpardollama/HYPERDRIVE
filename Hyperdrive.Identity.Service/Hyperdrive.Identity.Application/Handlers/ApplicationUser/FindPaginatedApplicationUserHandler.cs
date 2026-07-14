using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.Queries.ApplicationUser;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationUser;

public class FindPaginatedApplicationUserHandler : IRequestHandler<FindPaginatedApplicationUserQuery, ViewPage<ViewApplicationUser>>
{
    private readonly IApplicationUserManager _manager;

    public FindPaginatedApplicationUserHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewApplicationUser>> Handle(FindPaginatedApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedApplicationUser(
            request.ViewModel.Index,
            request.ViewModel.Size);

        return @page.ToPageViewModel();
    }
}