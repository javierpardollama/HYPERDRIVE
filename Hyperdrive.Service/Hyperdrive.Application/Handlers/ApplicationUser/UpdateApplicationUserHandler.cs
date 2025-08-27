using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationUser;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class UpdateApplicationUserHandler : IRequestHandler<UpdateApplicationUserCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}