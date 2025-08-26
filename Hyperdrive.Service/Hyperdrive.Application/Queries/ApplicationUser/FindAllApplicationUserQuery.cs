using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.ApplicationUser;

public class FindAllApplicationUserQuery : IRequest<IList<ViewCatalog>>
{
}