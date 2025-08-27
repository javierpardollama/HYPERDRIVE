using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class UpdateApplicationRoleHandler : IRequestHandler<UpdateApplicationRoleCommand, ViewApplicationRole>
{
    public Task<ViewApplicationRole> Handle(UpdateApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}