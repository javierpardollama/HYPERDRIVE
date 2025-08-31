using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

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