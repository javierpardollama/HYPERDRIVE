using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class EmailChangeHandler : IRequestHandler<EmailChangeCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(EmailChangeCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}