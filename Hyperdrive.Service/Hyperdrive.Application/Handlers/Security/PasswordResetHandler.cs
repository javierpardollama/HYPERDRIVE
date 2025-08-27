using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class PasswordResetHandler : IRequestHandler<PasswordResetCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}