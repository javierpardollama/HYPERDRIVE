using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Queries.ApplicationRole;

public class FindAllApplicationRoleQuery : IRequest<IList<ViewCatalog>>
{
}