using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.ApplicationUser;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class FindPaginatedApplicationUserHandler : IRequestHandler<FindPaginatedApplicationUserQuery, ViewPage<ViewApplicationUser>>
{
    public Task<ViewPage<ViewApplicationUser>> Handle(FindPaginatedApplicationUserQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}