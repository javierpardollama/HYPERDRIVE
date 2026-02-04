using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.ApplicationRole;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.ApplicationRole;

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