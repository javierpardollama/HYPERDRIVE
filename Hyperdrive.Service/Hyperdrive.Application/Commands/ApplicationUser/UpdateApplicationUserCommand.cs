using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationUser;

public class UpdateApplicationUserCommand : IRequest<ViewApplicationUser>
{
    public UpdateApplicationUser ViewModel { get; set; }
}