using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Security;

public class PasswordResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityPasswordReset ViewModel { get; set; }
}