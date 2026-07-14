using Hyperdrive.Identity.Application.ViewModels.Filters;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Queries.ApplicationUser;

public class FindPaginatedApplicationUserQuery : IRequest<ViewPage<ViewApplicationUser>>
{
    public FilterPageApplicationUser ViewModel { get; set; }
}