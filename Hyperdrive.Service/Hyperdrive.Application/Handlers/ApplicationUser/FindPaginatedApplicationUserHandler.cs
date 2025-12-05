using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.ApplicationUser;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

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