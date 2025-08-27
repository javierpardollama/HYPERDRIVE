using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class RefreshTokenResetHandler : IRequestHandler<RefreshTokenResetCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(RefreshTokenResetCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}