using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

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

        return [.. @roles.Select(x => x.ToCatalogViewModel())];
    }
}