using Hyperdrive.Storage.Application.ViewModels.Additions;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Commands.ApplicationUser;

public class AddApplicationUserCommand : IRequest<ViewCatalog>
{
    public AddApplicationUser ViewModel { get; set; }
}