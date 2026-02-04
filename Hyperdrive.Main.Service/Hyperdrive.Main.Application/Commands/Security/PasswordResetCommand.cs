using Hyperdrive.Main.Application.ViewModels.Security;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Security;

public class PasswordResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityPasswordReset ViewModel { get; set; }
}