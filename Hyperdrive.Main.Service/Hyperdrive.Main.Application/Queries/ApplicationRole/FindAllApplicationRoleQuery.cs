using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.ApplicationRole;

public class FindAllApplicationRoleQuery : IRequest<IList<ViewCatalog>>
{
}