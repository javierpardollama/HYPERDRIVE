using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.ApplicationUser;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.ApplicationUser;

public class FindAllApplicationUserHandler : IRequestHandler<FindAllApplicationUserQuery, IList<ViewCatalog>>
{
    private readonly IApplicationUserManager _manager;

    public FindAllApplicationUserHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }

    public async Task<IList<ViewCatalog>> Handle(FindAllApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var @users = await _manager.FindAllApplicationUser();

        return [.. @users.Select(x => x.ToViewModel())];
    }
}