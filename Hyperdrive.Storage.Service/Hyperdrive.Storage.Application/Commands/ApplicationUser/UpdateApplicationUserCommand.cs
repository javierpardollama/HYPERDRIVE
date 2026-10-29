using Hyperdrive.Storage.Application.ViewModels.Updates;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Commands.ApplicationUser;

public class UpdateApplicationUserCommand: IRequest<ViewCatalog>
{
    public UpdateApplicationUser ViewModel { get; set; }
}