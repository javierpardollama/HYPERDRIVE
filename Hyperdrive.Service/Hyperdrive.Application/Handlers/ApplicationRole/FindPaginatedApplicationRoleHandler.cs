using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class FindPaginatedApplicationRoleHandler : IRequestHandler<FindPaginatedApplicationRoleQuery, ViewPage<ViewApplicationRole>>
{
    public Task<ViewPage<ViewApplicationRole>> Handle(FindPaginatedApplicationRoleQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}