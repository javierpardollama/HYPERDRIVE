using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.ApplicationUser;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class FindAllApplicationUserHandler : IRequestHandler< FindAllApplicationUserQuery, IList<ViewCatalog>>
{
    private readonly IApplicationUserManager _manager;

    public FindAllApplicationUserHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }
    
    public async Task<IList<ViewCatalog>> Handle(FindAllApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var @users = await _manager.FindAllApplicationUser();

        return [.. @users.Select(x => x.ToCatalogViewModel())];
    }
}