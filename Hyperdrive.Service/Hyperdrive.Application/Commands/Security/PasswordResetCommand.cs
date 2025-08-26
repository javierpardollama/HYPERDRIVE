using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Security;

public class PasswordResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityPasswordReset ViewModel { get; set; }
}