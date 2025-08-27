using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class FindAllApplicationRoleHandler : IRequestHandler<FindAllApplicationRoleQuery, IList<ViewCatalog>>
{
    public Task<IList<ViewCatalog>> Handle(FindAllApplicationRoleQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}