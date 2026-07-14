using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Security;

public class NameChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityNameChange ViewModel { get; set; }
}