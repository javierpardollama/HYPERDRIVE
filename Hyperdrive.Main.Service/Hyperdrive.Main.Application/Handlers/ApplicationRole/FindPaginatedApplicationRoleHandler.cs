using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.ApplicationRole;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.ApplicationRole;

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