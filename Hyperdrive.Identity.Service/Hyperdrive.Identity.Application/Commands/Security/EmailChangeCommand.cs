using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Security;

public class EmailChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityEmailChange ViewModel { get; set; }
}