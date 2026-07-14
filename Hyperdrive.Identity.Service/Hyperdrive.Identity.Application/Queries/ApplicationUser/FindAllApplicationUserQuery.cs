using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Queries.ApplicationUser;

public class FindAllApplicationUserQuery : IRequest<IList<ViewCatalog>>
{
}