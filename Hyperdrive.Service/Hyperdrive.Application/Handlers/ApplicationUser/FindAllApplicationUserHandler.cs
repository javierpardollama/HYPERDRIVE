using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.ApplicationUser;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class FindAllApplicationUserHandler : IRequestHandler< FindAllApplicationUserQuery, IList<ViewCatalog>>
{
    public Task<IList<ViewCatalog>> Handle(FindAllApplicationUserQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}