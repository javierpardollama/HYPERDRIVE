using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;
using System.Collections.Generic;

namespace Hyperdrive.Main.Application.Queries.ApplicationRole;

public class FindAllApplicationRoleQuery : IRequest<IList<ViewCatalog>>
{
}