using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Security;

public class NameChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityNameChange ViewModel { get; set; }
}