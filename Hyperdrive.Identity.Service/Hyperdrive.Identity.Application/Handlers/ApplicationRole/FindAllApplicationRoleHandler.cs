using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.Queries.ApplicationRole;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationRole;

public class FindAllApplicationRoleHandler : IRequestHandler<FindAllApplicationRoleQuery, IList<ViewCatalog>>
{
    private readonly IApplicationRoleManager _manager;

    public FindAllApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<IList<ViewCatalog>> Handle(FindAllApplicationRoleQuery request, CancellationToken cancellationToken)
    {
        var @roles = await _manager.FindAllApplicationRole();

        return [.. @roles.Select(x => x.ToViewModel())];
    }
}