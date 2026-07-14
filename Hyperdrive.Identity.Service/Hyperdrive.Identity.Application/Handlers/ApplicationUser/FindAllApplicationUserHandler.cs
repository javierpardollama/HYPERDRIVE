using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.Queries.ApplicationUser;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationUser;

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