using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.ApplicationUser;

public class FindPaginatedApplicationUserQuery : IRequest<ViewPage<ViewApplicationUser>>
{
    public FilterPageApplicationUser ViewModel { get; set; }
}