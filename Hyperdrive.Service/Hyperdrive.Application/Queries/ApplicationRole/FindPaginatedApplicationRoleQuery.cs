using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.ApplicationRole;

public class FindPaginatedApplicationRoleQuery : IRequest<ViewPage<ViewApplicationRole>>
{
    public FilterPageApplicationRole ViewModel { get; set; }
}