using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.ApplicationUser;

public class FindPaginatedApplicationUserQuery : IRequest<ViewPage<ViewApplicationUser>>
{
    public FilterPageApplicationUser ViewModel { get; set; }
}