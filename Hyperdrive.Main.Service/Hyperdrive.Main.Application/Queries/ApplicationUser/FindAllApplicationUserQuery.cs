using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;
using System.Collections.Generic;

namespace Hyperdrive.Main.Application.Queries.ApplicationUser;

public class FindAllApplicationUserQuery : IRequest<IList<ViewCatalog>>
{
}