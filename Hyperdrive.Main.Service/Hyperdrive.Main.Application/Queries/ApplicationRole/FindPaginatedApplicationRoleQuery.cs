using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.ApplicationRole;

public class FindPaginatedApplicationRoleQuery : IRequest<ViewPage<ViewApplicationRole>>
{
    public FilterPageApplicationRole ViewModel { get; set; }
}