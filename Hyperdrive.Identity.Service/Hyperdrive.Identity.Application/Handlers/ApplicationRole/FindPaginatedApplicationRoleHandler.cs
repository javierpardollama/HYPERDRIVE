using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.Queries.ApplicationRole;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationRole;

public class FindPaginatedApplicationRoleHandler : IRequestHandler<FindPaginatedApplicationRoleQuery, ViewPage<ViewApplicationRole>>
{
    private readonly IApplicationRoleManager _manager;

    public FindPaginatedApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewApplicationRole>> Handle(FindPaginatedApplicationRoleQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedApplicationRole(
            request.ViewModel.Index,
            request.ViewModel.Size);

        return @page.ToPageViewModel();
    }
}