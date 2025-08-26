using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Security;

public class PasswordChangeCommand : IRequest<ViewApplicationUser>
{
    public SecurityPasswordChange ViewModel { get; set; }
}