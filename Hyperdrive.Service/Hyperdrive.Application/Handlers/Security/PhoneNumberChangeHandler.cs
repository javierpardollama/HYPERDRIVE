using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.Security;

public class PhoneNumberChangeHandler : IRequestHandler<PhoneNumberChangeCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(PhoneNumberChangeCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}