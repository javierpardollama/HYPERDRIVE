using Hyperdrive.Main.Application.ViewModels.Security;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Security;

public class NameChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityNameChange ViewModel { get; set; }
}