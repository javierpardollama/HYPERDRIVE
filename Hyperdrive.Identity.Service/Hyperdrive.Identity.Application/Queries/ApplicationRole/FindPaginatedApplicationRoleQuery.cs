using Hyperdrive.Identity.Application.ViewModels.Filters;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Queries.ApplicationRole;

public class FindPaginatedApplicationRoleQuery : IRequest<ViewPage<ViewApplicationRole>>
{
    public FilterPageApplicationRole ViewModel { get; set; }
}