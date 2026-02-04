using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.ApplicationUser;

public class UpdateApplicationUserCommand : IRequest<ViewApplicationUser>
{
    public UpdateApplicationUser ViewModel { get; set; }
}