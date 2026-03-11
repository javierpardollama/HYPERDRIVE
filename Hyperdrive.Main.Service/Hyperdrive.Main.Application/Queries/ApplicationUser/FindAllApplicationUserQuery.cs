using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.ApplicationUser;

public class FindAllApplicationUserQuery : IRequest<IList<ViewCatalog>>
{
}